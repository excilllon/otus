using System.Numerics;
using TestCaseReader;

namespace Lesson3.Test
{
    public class FiboTests
    {
        private IEnumerable<TestCase> _testCases = Array.Empty<TestCase>();
        [SetUp]
        public void Setup()
        {
            var reader = new CaseReader();
            var testCasesPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Cases\\Fibo");
            Assert.True(Directory.Exists(testCasesPath), "Папка с тестовыми файлами не найдена");
            _testCases = reader.ReadCases(testCasesPath);
        }

        private void TestInternalBig(Func<int, BigInteger> fiboFunc)
        {
            foreach (var testCase in _testCases)
            {
                if (int.TryParse(testCase.In, out var value) && BigInteger.TryParse(testCase.Expected, out var expected))
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
        public void TestFiboIterations()
        {
            TestInternalBig(Fibo.FiboWithIterations);
        }

        [Test]
        public void TestFiboRecursive()
        {
            TestInternalBig(Fibo.FiboR);
        }
        
        [Test]
        public void TestFiboGolden()
        {
            foreach (var testCase in _testCases)
            {
                if (int.TryParse(testCase.In, out var value) && double.TryParse(testCase.Expected, out var expected))
                {
                    var actual = Fibo.FiboGolden(value);
                    Assert.AreEqual(expected, actual, 0.0001,
                        $"тестовый случай {testCase.TestCaseNumber} не пройден, ожидается: {expected}, факт: {actual}");
                    TestContext.Out.WriteLine(
                        $"УСПЕШО: Тест: {testCase.TestCaseNumber}, ожидается: {expected}, факт: {actual}");
                }
                else Assert.Fail($"Тестовый файл {testCase.TestCaseNumber} не удалось распарсить");
            }

            Assert.Pass();
        }
    }
}