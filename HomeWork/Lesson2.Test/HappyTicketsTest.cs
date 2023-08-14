using System.Diagnostics;
using TestCaseReader;

namespace Lesson2.Test
{
    public class Tests
    {
        private IEnumerable<TestCase> _testCases = Array.Empty<TestCase>();
        [SetUp]
        public void Setup()
        {
            var reader = new CaseReader();
            var testCasesPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Cases");
            Assert.True(Directory.Exists(testCasesPath), "Папка с тестовыми файлами не найдена");
            _testCases = reader.ReadCases(testCasesPath);
        }

        [Test]
        public void TestFromFiles()
        {
            foreach (var testCase in _testCases)
            {
                if (int.TryParse(testCase.In, out var value) && long.TryParse(testCase.Expected, out var expected))
                {
                    var actual = HappyTickets.GetCount(value);
                    Assert.AreEqual(expected, actual, 0.0001, $"тестовый случай {testCase.TestCaseNumber} не пройден, ожидается: {expected}, факт: {actual}");
                    TestContext.Out.WriteLine($"УСПЕШО: Тест: {testCase.TestCaseNumber}, ожидается: {expected}, факт: {actual}");
                }
                else Assert.Fail($"Тестовый файл {testCase.TestCaseNumber} не удалось распарсить");
            }
            Assert.Pass();
        }
    }
}