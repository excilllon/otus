using TestCaseReader;

namespace Lesson3.Test
{
    public class PrimeTests
    {
        private IEnumerable<TestCase> _testCases = Array.Empty<TestCase>();
        [SetUp]
        public void Setup()
        {
            var reader = new CaseReader();
            var testCasesPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Cases\\Primes");
            Assert.True(Directory.Exists(testCasesPath), "Папка с тестовыми файлами не найдена");
            _testCases = reader.ReadCases(testCasesPath);
        }

        private void TestInternal(Func<long, long> fiboFunc)
        {
            foreach (var testCase in _testCases)
            {
                if (int.TryParse(testCase.In, out var value) && long.TryParse(testCase.Expected, out var expected))
                {
                    var actual = fiboFunc(value);
                    var equal = expected == actual;
                    Assert.IsTrue(equal, $"тестовый случай {testCase.TestCaseNumber} не пройден, ожидается: {expected}, факт: {actual}");
                    TestContext.Out.WriteLine(
                        $"УСПЕШО: Тест: {testCase.TestCaseNumber}, ожидается: {expected}, факт: {actual}");
                }
                else Assert.Fail($"Тестовый файл {testCase.TestCaseNumber} не удалось распарсить");
            }

            Assert.Pass();
        }

        
        [Test]
        public void TestCountPrimesIterations()
        {
            TestInternal(PrimeNumbers.CountPrimesIterations);
        }
        
        [Test]
        public void TestCountEratosthenes()
        {
            TestInternal(PrimeNumbers.CountEratosthenes);
        }
        
        [Test]
        public void TestCountEratosthenesOptimized()
        {
            TestInternal(PrimeNumbers.CountEratosthenesOptimized);
        }
    }
}
