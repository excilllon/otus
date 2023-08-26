using System.Numerics;
using BenchmarkDotNet.Attributes;

namespace Lesson3.Benchmarks
{
    [SimpleJob]
    public class PrimesBenchmark
    {
        [Params(10, 1, 2, 3, 4, 5)]
        public int PrimeInVal;

        [Benchmark]
        public long CountPrimesIterations() => PrimeNumbers.CountPrimesIterations(PrimeInVal);
        
        [Benchmark]
        public long CountEratosthenes() => PrimeNumbers.CountEratosthenes(PrimeInVal);
        
        [Benchmark]
        public long CountEratosthenesOptimized() => PrimeNumbers.CountEratosthenesOptimized(PrimeInVal);

    }
}
