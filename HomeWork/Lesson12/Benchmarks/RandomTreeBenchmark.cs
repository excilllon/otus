using BenchmarkDotNet.Attributes;

namespace Lesson12.Benchmarks
{
    [SimpleJob]
    [WarmupCount(6)]
    [IterationCount(6)]
    public class RandomTreeBenchmark
    {
        [Params(1000)]
        public int N;
        private RandomTree _randomRandomTreeForInsert;
        private RandomTree _sortedRandomTreeForInsert;

        private RandomTree _randomRandomTreeForRemove;
        private RandomTree _sortedRandomTreeForRemove;

        private RandomTree _randomRandomTreeForSearch;
        private RandomTree _sortedRandomTreeForSearch;

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
            _randomRandomTreeForInsert = new RandomTree();
            _sortedRandomTreeForInsert = new RandomTree();

            _sortedRandomTreeForSearch = new RandomTree();
            _sortedRandomTreeForRemove = new RandomTree();

            _randomRandomTreeForSearch = new RandomTree();
            _randomRandomTreeForRemove = new RandomTree();

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
                _randomRandomTreeForSearch.Insert(data);
                _randomRandomTreeForRemove.Insert(data);
            }
            foreach (var data in _sortedData)
            {
                _sortedRandomTreeForSearch.Insert(data);
                _sortedRandomTreeForRemove.Insert(data);
            }
        }

        [Benchmark]
        public void RandomTreeRandomInsert()
        {
            foreach (var data in _randomData)
            {
                _randomRandomTreeForInsert.Insert(data);
            }
        }

        [Benchmark]
        public void RandomTreeRandomSearch()
        {
            foreach (var data in _randomSearchOrRemove)
            {
                _randomRandomTreeForSearch.Search(data);
            }
        }

        [Benchmark]
        public void RandomTreeRandomSearch1000Times()
        {
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    _randomRandomTreeForSearch.Search(j);
                }
            }
        }
        
        [Benchmark]
        public void RandomTreeRandomRemove()
        {
            foreach (var data in _randomSearchOrRemove)
            {
                _randomRandomTreeForRemove.Remove(data);
            }
        }

        [Benchmark]
        public void RandomTreeSortedInsert()
        {
            foreach (var data in _sortedData)
            {
                _sortedRandomTreeForInsert.Insert(data);
            }
        }

        [Benchmark]
        public void RandomTreeSortedSearch()
        {
            foreach (var data in _randomSearchOrRemoveInSorted)
            {
                _sortedRandomTreeForSearch.Search(data);
            }
        }

        [Benchmark]
        public void RandomTreeSortedSearch1000Times()
        {
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    _sortedRandomTreeForSearch.Search(j);
                }
            }
        }

        [Benchmark]
        public void RandomTreeSortedRemove()
        {
            foreach (var data in _randomSearchOrRemoveInSorted)
            {
                _sortedRandomTreeForRemove.Remove(data);
            }
        }
    }
}
