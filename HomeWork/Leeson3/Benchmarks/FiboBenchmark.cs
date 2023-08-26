using System.Numerics;
using BenchmarkDotNet.Attributes;

namespace Lesson3.Benchmarks
{
    [SimpleJob]
    public class FiboBenchmark
    {
        [Params(0, 1, 3, 4, 5, 10)]
        public int FiboInVal;

        [Benchmark]
        public BigInteger FiboWithIterations() => Fibo.FiboWithIterations(FiboInVal);

        [Benchmark]
        public BigInteger FiboR() => Fibo.FiboR(FiboInVal);

        [Benchmark]
        public double FiboGolden() => Fibo.FiboGolden(FiboInVal);

    }
}
