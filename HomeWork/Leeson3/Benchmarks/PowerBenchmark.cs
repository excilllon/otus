using System.Numerics;
using BenchmarkDotNet.Attributes;

namespace Lesson3.Benchmarks
{
    [SimpleJob]
    public class PowerBenchmark
    {
        [Params(2, 1.001, 1.0001, 1.00001, 1.000001)]
        public double PowerInVal;

        [Params(10, 0, 1000, 10000, 100000)]
        public int PowerInPowVal;
        
        [Benchmark]
        public double PowerWithIterations() => Power.PowerWithIterations(PowerInVal, PowerInPowVal);
        
        [Benchmark]
        public double PowBinary() => Power.PowBinary(PowerInVal, PowerInPowVal);

    }
}
