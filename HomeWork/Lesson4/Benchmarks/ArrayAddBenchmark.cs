using BenchmarkDotNet.Attributes;
using Lesson4.Arrays;

namespace Lesson4.Benchmarks
{
    [SimpleJob]
    [WarmupCount(6)]
    [IterationCount(6)]
    public class ArrayAddBenchmark
    {
        private SingleArray<int> singleArray = new();
        private FactorArray<int> factorArray = new();
        private VectorArray<int> vecArray = new();
        private MatrixArray<int> matrixArray10 = new(10);
        private ArrayListAdapter arrayList = new();
        private int[] data;
        [Params(1000, 10000, 100000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            data = new int[N];
            var random = new Random(42);
            for (int i = 0; i < N; i++)
            {
                data[i] = random.Next();
            }
        }

        [IterationCleanup]
        public void CleanUpAfterIteration()
        {
            singleArray = new();
            factorArray = new();
            vecArray = new();
            matrixArray10 = new(10);
            arrayList = new();
        }

        [Benchmark]
        public void AddSingle()
        {
            foreach (var d in data)
            {
                singleArray.Add(d, 0);
            }
        }

        [Benchmark]
        public void AddFactor()
        {
            foreach (var d in data)
            {
                factorArray.Add(d, 0);
            }
        }

        [Benchmark]
        public void AddVector()
        {
            foreach (var d in data)
            {
                vecArray.Add(d, 0);
            }
        }

        [Benchmark]
        public void AddMatrix()
        {
            for (int i = 0; i < N; i++)
            {
                matrixArray10.Add(data[i], 0);
            }
        }

        [Benchmark]
        public void AddArrayList()
        {
            foreach (var d in data)
            {
                arrayList.Add(d, 0);
            }
        }
    }
}
