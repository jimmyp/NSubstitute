using System;
using NSubstitute.Core;
using NSubstitute.Specs.Infrastructure;
using NUnit.Framework;

namespace NSubstitute.Specs
{
    public class ResultSetterSpecs
    {
        public abstract class Concern : ConcernFor<ResultSetter>
        {
            protected ICall _call;
            protected ICallStack _callStack;
            protected ICallResults _configuredResults;
            protected ICallSpecificationFactory _callSpecificationFactory;
            protected IReturn _returnValue;

            public override void Context()
            {
                _callStack = mock<ICallStack>();
                _call = mock<ICall>();
                _configuredResults = mock<ICallResults>();
                _callSpecificationFactory = mock<ICallSpecificationFactory>();

                _returnValue = mock<IReturn>();
                _callStack.stub(x => x.Pop()).Return(_call);
            }

            public override ResultSetter CreateSubjectUnderTest()
            {
                return new ResultSetter(_callStack, _configuredResults, _callSpecificationFactory);
            }
        }
        
        public class When_the_return_value_for_the_last_call_is_set : Concern
        {
            ICallSpecification _callSpecification;

            [Test]
            public void Should_remove_the_call_from_those_recorded_and_add_it_to_configured_results()
            {
                _configuredResults.received(x => x.SetResult(_callSpecification, _returnValue));
            }

            public override void Because()
            {
                sut.SetResultForLastCall(_returnValue, MatchArgs.AsSpecifiedInCall);
            }

            public override void Context()
            {
                base.Context();
                _callSpecification = mock<ICallSpecification>();
                _callSpecificationFactory.stub(x => x.CreateFrom(_call, MatchArgs.AsSpecifiedInCall)).Return(_callSpecification);
            }
        }

        public class When_the_return_value_for_the_last_call_with_any_arguments_is_set : Concern
        {
            private ICallSpecification _callWithAnyArgsSpecification;

            [Test]
            public void Should_remove_the_call_from_those_record_and_add_that_call_with_any_args_to_the_configured_results()
            {
                _configuredResults.received(x => x.SetResult(_callWithAnyArgsSpecification, _returnValue)); 
            }

            public override void Because()
            {
                sut.SetResultForLastCall(_returnValue, MatchArgs.Any);
            }

            public override void Context()
            {
                base.Context();
                _callWithAnyArgsSpecification = mock<ICallSpecification>();
                _callSpecificationFactory.stub(x => x.CreateFrom(_call, MatchArgs.Any)).Return(_callWithAnyArgsSpecification);
            }
        }
    }
}