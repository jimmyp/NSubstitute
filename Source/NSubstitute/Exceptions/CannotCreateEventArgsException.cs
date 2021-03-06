using System;
using System.Runtime.Serialization;

namespace NSubstitute.Exceptions
{
    public class CannotCreateEventArgsException : Exception
    {
        public CannotCreateEventArgsException() { }
        public CannotCreateEventArgsException(string message) : base(message) { }
        public CannotCreateEventArgsException(string message, Exception innerException) : base(message, innerException) { }
        protected CannotCreateEventArgsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}