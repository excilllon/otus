using BenchmarkDotNet.Attributes;

namespace Lesson9.Benchmarks
{
    [SimpleJob]
    [WarmupCount(10)]
    [IterationCount(10)]
    public class SortingsBenchmark
    {
        [Params(100, 1000, 10000, 100000, 1000000)]
        public int N;
        private int[] _data;

        [GlobalSetup]
        public void Setup()
        {
            _data = new int[N];
            var random = new Random(42);
            for (var i = 0; i < N; i++)
            {
                _data[i] = random.Next(0, 999);
            }
        }

        [IterationSetup]
        public void SetupIteration()
        {
            _data = new int[N];
            var random = new Random(42);
            for (var i = 0; i < N; i++)
            {
                _data[i] = random.Next(0, 999);
            }
        }

        [Benchmark]
        public void BucketSort()
        {
            var sortings = new Sortings();
            sortings.SetArray(_data);
            sortings.BucketSort();
        }
        
        [Benchmark]
        public void CountingSort()
        {
            var sortings = new Sortings();
            sortings.SetArray(_data);
            sortings.CountingSort();
        } 
        
        [Benchmark]
        public void RadixSort()
        {
            var sortings = new Sortings();
            sortings.SetArray(_data);
            sortings.RadixSort();
        } 
    }
}
