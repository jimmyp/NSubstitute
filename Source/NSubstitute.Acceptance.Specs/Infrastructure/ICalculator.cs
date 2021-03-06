using System;

namespace NSubstitute.Acceptance.Specs.Infrastructure
{
    public interface ICalculator
    {
        void SwitchOn();
        int Add(int a, int b);
        int Subtract(int a, int b);
        string Name { get; set; }
        DateTime Now { get; }
        string Firmware { set; }
    }
}