using BenchmarkDotNet.Attributes;

namespace Lesson12.Benchmarks
{
    [SimpleJob]
    [WarmupCount(6)]
    [IterationCount(6)]
    public class SplayTreeBenchmark
    {
        [Params(1000)]
        public int N;
        private SplayTree _randomSplayTreeForInsert;
        private SplayTree _sortedSplayTreeForInsert;

        private SplayTree _randomSplayTreeForRemove;
        private SplayTree _sortedSplayTreeForRemove;

        private SplayTree _randomSplayTreeForSearch;
        private SplayTree _sortedSplayTreeForSearch;

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
            _randomSplayTreeForInsert = new SplayTree();
            _sortedSplayTreeForInsert = new SplayTree();

            _sortedSplayTreeForSearch = new SplayTree();
            _sortedSplayTreeForRemove = new SplayTree();

            _randomSplayTreeForSearch = new SplayTree();
            _randomSplayTreeForRemove = new SplayTree();

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
                _randomSplayTreeForSearch.Insert(data);
                _randomSplayTreeForRemove.Insert(data);
            }
            foreach (var data in _sortedData)
            {
                _sortedSplayTreeForSearch.Insert(data);
                _sortedSplayTreeForRemove.Insert(data);
            }
        }

        [Benchmark]
        public void SplayTreeRandomInsert()
        {
            foreach (var data in _randomData)
            {
                _randomSplayTreeForInsert.Insert(data);
            }
        }

        [Benchmark]
        public void SplayTreeRandomSearch()
        {
            foreach (var data in _randomSearchOrRemove)
            {
                _randomSplayTreeForSearch.Search(data);
            }
        }

        [Benchmark]
        public void SplayTreeRandomSearch1000Times()
        {
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    _randomSplayTreeForSearch.Search(j);
                }
            }
        }
        
        [Benchmark]
        public void SplayTreeRandomRemove()
        {
            foreach (var data in _randomSearchOrRemove)
            {
                _randomSplayTreeForRemove.Remove(data);
            }
        }

        [Benchmark]
        public void SplayTreeSortedInsert()
        {
            foreach (var data in _sortedData)
            {
                _sortedSplayTreeForInsert.Insert(data);
            }
        }

        [Benchmark]
        public void SplayTreeSortedSearch()
        {
            foreach (var data in _randomSearchOrRemoveInSorted)
            {
                _sortedSplayTreeForSearch.Search(data);
            }
        }

        [Benchmark]
        public void SplayTreeSortedSearch1000Times()
        {
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    _sortedSplayTreeForSearch.Search(j);
                }
            }
        }

        [Benchmark]
        public void SplayTreeSortedRemove()
        {
            foreach (var data in _randomSearchOrRemoveInSorted)
            {
                _sortedSplayTreeForRemove.Remove(data);
            }
        }
    }
}
