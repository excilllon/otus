using System.Globalization;
using TestCaseReader;

namespace Lesson3.Test
{
    public class PowerTests
    {
        private IEnumerable<TestCase> _testCases = Array.Empty<TestCase>();
        [SetUp]
        public void Setup()
        {
            var reader = new CaseReader();
            var testCasesPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Cases\\Power");
            Assert.True(Directory.Exists(testCasesPath), "Папка с тестовыми файлами не найдена");
            _testCases = reader.ReadCases(testCasesPath);
        }

        private void TestInternal(Func<double, long, double> func)
        {
            foreach (var testCase in _testCases)
            {
                var values = testCase.In.Split(Environment.NewLine);
                if (double.TryParse(values[0],NumberStyles.Number, 
                        new NumberFormatInfo(){NumberDecimalSeparator = "."}, out var value) &&
                      long.TryParse(values[1], out var power) 
                    && double.TryParse(testCase.Expected,NumberStyles.Number, 
                          new NumberFormatInfo(){NumberDecimalSeparator = "."}, out var expected))
                {
                    var actual = func(value, power);
                    Assert.AreEqual(expected, actual, 0.0001,
                        $"тестовый случай {testCase.TestCaseNumber} не пройден, ожидается: {expected}, факт: {actual}");
                    TestContext.Out.WriteLine(
                        $"УСПЕШО: Тест: {testCase.TestCaseNumber}, ожидается: {expected}, факт: {actual}");
                }
                else Assert.Fail($"Тестовый файл {testCase.TestCaseNumber} не удалось распарсить");
            }

            Assert.Pass();
        }

        [Test]
        public void TestPowerIterations()
        {
            TestInternal(Power.PowerWithIterations);
        }

        [Test]
        public void TestPowBinary()
        {
            TestInternal(Power.PowBinary);
        }
    }
}