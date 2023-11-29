using BenchmarkDotNet.Attributes;

namespace Lesson8.Benchmarks
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
                _data[i] = random.Next();
            }
        }

        [IterationSetup]
        public void SetupIteration()
        {
            _data = new int[N];
            var random = new Random(42);
            for (var i = 0; i < N; i++)
            {
                _data[i] = random.Next();
            }
        }

        [Benchmark]
        public void QuickSort()
        {
            var sortings = new Sortings();
            sortings.SetArray(_data);
            sortings.QuickSort();
        }
        
        [Benchmark]
        public void MergeSort()
        {
            var sortings = new Sortings();
            sortings.SetArray(_data);
            sortings.MergeSort();
        } 
    }
}
