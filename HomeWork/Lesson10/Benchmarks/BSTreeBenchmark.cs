using BenchmarkDotNet.Attributes;

namespace Lesson10.Benchmarks
{
    [SimpleJob]
    [WarmupCount(6)]
    [IterationCount(6)]
    public class BSTreeBenchmark
    {
        [Params(100, 1000, 10000, 100000)]
        public int N;
        private BSTree _randomBstTreeForInsert;
        private BSTree _sortedBstTreeForInsert;

        private BSTree _randomBstTreeForRemove;
        private BSTree _sortedBstTreeForRemove;

        private BSTree _randomBstTreeForSearch;
        private BSTree _sortedBstTreeForSearch;

        private int[] _randomData;
        private int[] _sortedData;

        private int[] _randomSearchOrRemoveInSorted;
        private int[] _randomSearchOrRemove;

        [GlobalSetup]
        public void Setup()
        {
            var random = new Random(42);
            _randomData = new int[N];
            _sortedData = new int[N];
            for (var i = 0; i < N; i++)
            {
                _randomData[i] = random.Next();
                _sortedData[i] = random.Next();
            }
            Array.Sort(_sortedData);
        }

        [IterationSetup]
        public void SetupIteration()
        {
            _randomBstTreeForInsert = new BSTree();
            _sortedBstTreeForInsert = new BSTree();

            _sortedBstTreeForSearch = new BSTree();
            _sortedBstTreeForRemove = new BSTree();

            _randomBstTreeForSearch = new BSTree();
            _randomBstTreeForRemove = new BSTree();

            _randomSearchOrRemoveInSorted = new int[N / 10];
            _randomSearchOrRemove = new int[N / 10];
            var random = new Random(5);
            for (var i = 0; i < N / 10; i++)
            {
                _randomSearchOrRemoveInSorted[i] = _sortedData[random.Next(0, N)];
                _randomSearchOrRemove[i] = _randomData[random.Next(0, N)];
            }
            foreach (var data in _randomData)
            {
                _randomBstTreeForSearch.Insert(data);
                _randomBstTreeForRemove.Insert(data);
            }
            foreach (var data in _sortedData)
            {
                _sortedBstTreeForSearch.Insert(data);
                _sortedBstTreeForRemove.Insert(data);
            }
        }

        [Benchmark]
        public void BSTreeRandomInsert()
        {
            foreach (var data in _randomData)
            {
                _randomBstTreeForInsert.Insert(data);
            }
        }

        [Benchmark]
        public void BSTreeRandomSearch()
        {
            foreach (var data in _randomSearchOrRemove)
            {
                _randomBstTreeForSearch.Search(data);
            }
        }
        
        [Benchmark]
        public void BSTreeRandomRemove()
        {
            foreach (var data in _randomSearchOrRemove)
            {
                _randomBstTreeForRemove.Remove(data);
            }
        }

        [Benchmark]
        public void BSTreeSortedInsert()
        {
            foreach (var data in _sortedData)
            {
                _sortedBstTreeForInsert.Insert(data);
            }
        }

        [Benchmark]
        public void BSTreeSortedSearch()
        {
            foreach (var data in _randomSearchOrRemoveInSorted)
            {
                _sortedBstTreeForSearch.Search(data);
            }
        }
        
        [Benchmark]
        public void BSTreeSortedRemove()
        {
            foreach (var data in _randomSearchOrRemoveInSorted)
            {
                _sortedBstTreeForRemove.Remove(data);
            }
        }
    }
}
