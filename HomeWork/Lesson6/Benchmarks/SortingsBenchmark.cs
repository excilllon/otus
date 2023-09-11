using BenchmarkDotNet.Attributes;

namespace Lesson6.Benchmarks
{
    [SimpleJob]
    [WarmupCount(6)]
    [IterationCount(6)]
    public class SortingsBenchmark
    {
        [Params(100, 1000, 10000, 100000)]
        public int N;
        private int[] data;

        [GlobalSetup]
        public void Setup()
        {
            data = new int[N];
            var random = new Random(42);
            for (var i = 0; i < N; i++)
            {
                data[i] = random.Next();
            }
        }

        [IterationSetup]
        public void SetupIteration()
        {
            data = new int[N];
            var random = new Random(42);
            for (var i = 0; i < N; i++)
            {
                data[i] = random.Next();
            }
        }

        [Benchmark]
        public void Bubble()
        {
            var sortings = new Sortings();
            sortings.SetArray(data);
            sortings.Bubble();
        }
        
        [Benchmark]
        public void BubbleWithSelection()
        {
            var sortings = new Sortings();
            sortings.SetArray(data);
            sortings.BubbleWithSelection();
        } 
        
        [Benchmark]
        public void Insertion()
        {
            var sortings = new Sortings();
            sortings.SetArray(data);
            sortings.Insertion();
        }
        
        [Benchmark]
        public void InsertionShift()
        {
            var sortings = new Sortings();
            sortings.SetArray(data);
            sortings.InsertionShift();
        } 
        
        [Benchmark]
        public void InsertionBinary()
        {
            var sortings = new Sortings();
            sortings.SetArray(data);
            sortings.InsertionBinary();
        } 
        
        [Benchmark]
        public void Shell()
        {
            var sortings = new Sortings();
            sortings.SetArray(data);
            sortings.Shell();
        } 
        
        [Benchmark]
        public void ShellHibbard()
        {
            var sortings = new Sortings();
            sortings.SetArray(data);
            sortings.ShellHibbard();
        }
        
        [Benchmark]
        public void ShellCiura()
        {
            var sortings = new Sortings();
            sortings.SetArray(data);
            sortings.ShellCiura();
        }
    }
}
