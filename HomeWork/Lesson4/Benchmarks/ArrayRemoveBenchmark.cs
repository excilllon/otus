using BenchmarkDotNet.Attributes;
using Lesson4.Arrays;

namespace Lesson4.Benchmarks
{
    [SimpleJob]
    [WarmupCount(6)]
    [IterationCount(6)]
    public class ArrayRemoveBenchmark
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
            for (var i = 0; i < N; i++)
            {
                data[i] = random.Next();
                singleArray.Add(data[i], N);
                factorArray.Add(data[i], N);
                vecArray.Add(data[i], N);
                matrixArray10.Add(data[i], N);
                arrayList.Add(data[i], arrayList.Length);
            }
        }

        [IterationSetup]
        public void SetupIteration()
        {
            singleArray = new();
            factorArray = new();
            vecArray = new();
            matrixArray10 = new(10);
            arrayList = new();
            for (var i = 0; i < N; i++)
            {
                singleArray.Add(data[i], N);
                factorArray.Add(data[i], N);
                vecArray.Add(data[i], N);
                matrixArray10.Add(data[i], N);
                arrayList.Add(data[i], arrayList.Length);
            }
        }

        [Benchmark]
        public void RemoveSingle()
        {
            for (var i = 0; i < N; i++)
            {
                singleArray.Remove(0);
            }
        }

        [Benchmark]
        public void RemoveFactor()
        {
            for (var i = 0; i < N; i++)
            {
                factorArray.Remove(0);
            }
        }

        [Benchmark]
        public void RemoveVector()
        {
            for (var i = 0; i < N; i++)
            {
                vecArray.Remove(0);
            }
        }

        [Benchmark]
        public void RemoveMatrix()
        {
            for (var i = 0; i < N; i++)
            {
                matrixArray10.Remove(0);
            }
        }

        [Benchmark]
        public void RemoveArrayList()
        {
            for (var i = 0; i < N; i++)
            {
                arrayList.Remove(0);
            }
        }
    }
}
