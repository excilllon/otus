using BenchmarkDotNet.Attributes;

namespace Lesson7.Benchmarks
{
    [SimpleJob]
    [WarmupCount(6)]
    [IterationCount(6)]
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
        public void Selection()
        {
            var sortings = new Sortings();
            sortings.SetArray(_data);
            sortings.SelectionSort();
        }
        
        [Benchmark]
        public void Heap()
        {
            var sortings = new Sortings();
            sortings.SetArray(_data);
            sortings.HeapSort();
        } 
    }
}
