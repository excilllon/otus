using BenchmarkDotNet.Attributes;

namespace Lesson10.Benchmarks
{
    [SimpleJob]
    [WarmupCount(6)]
    [IterationCount(6)]
    public class AVLTreeBenchmark
    {
        [Params(100, 1000, 10000, 100000)]
        public int N;
        private AVLTree _randomAvlTreeForInsert;
        private AVLTree _sortedAvlTreeForInsert;

        private AVLTree _randomAvlTreeForRemove;
        private AVLTree _sortedAvlTreeForRemove;

        private AVLTree _randomAvlTreeForSearch;
        private AVLTree _sortedAvlTreeForSearch;

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
            _randomAvlTreeForInsert = new AVLTree();
            _sortedAvlTreeForInsert = new AVLTree();

            _sortedAvlTreeForSearch = new AVLTree();
            _sortedAvlTreeForRemove = new AVLTree();

            _randomAvlTreeForSearch = new AVLTree();
            _randomAvlTreeForRemove = new AVLTree();

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
                _randomAvlTreeForSearch.Insert(data);
                _randomAvlTreeForRemove.Insert(data);
            }
            foreach (var data in _sortedData)
            {
                _sortedAvlTreeForSearch.Insert(data);
                _sortedAvlTreeForRemove.Insert(data);
            }
        }

        [Benchmark]
        public void AvlTreeRandomInsert()
        {
            foreach (var data in _randomData)
            {
                _randomAvlTreeForInsert.Insert(data);
            }
        }

        [Benchmark]
        public void AvlTreeRandomSearch()
        {
            foreach (var data in _randomSearchOrRemove)
            {
                _randomAvlTreeForSearch.Search(data);
            }
        }
        
        [Benchmark]
        public void AvlTreeRandomRemove()
        {
            foreach (var data in _randomSearchOrRemove)
            {
                _randomAvlTreeForRemove.Remove(data);
            }
        }

        [Benchmark]
        public void AvlTreeSortedInsert()
        {
            foreach (var data in _sortedData)
            {
                _sortedAvlTreeForInsert.Insert(data);
            }
        }

        [Benchmark]
        public void AvlTreeSortedSearch()
        {
            foreach (var data in _randomSearchOrRemoveInSorted)
            {
                _sortedAvlTreeForSearch.Search(data);
            }
        }
        
        [Benchmark]
        public void AvlTreeSortedRemove()
        {
            foreach (var data in _randomSearchOrRemoveInSorted)
            {
                _sortedAvlTreeForRemove.Remove(data);
            }
        }
    }
}
