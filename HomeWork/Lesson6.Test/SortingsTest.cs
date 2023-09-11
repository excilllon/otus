using TestCaseReader;

namespace Lesson6.Test
{
    public class Tests
    {
        private IEnumerable<TestCase> _testCasesRandom = Array.Empty<TestCase>();
        private IEnumerable<TestCase> _testCasesDigits = Array.Empty<TestCase>();
        private IEnumerable<TestCase> _testCasesSorted = Array.Empty<TestCase>();
        private IEnumerable<TestCase> _testCasesRevers = Array.Empty<TestCase>();
        [SetUp]
        public void Setup()
        {
            var reader = new CaseReader();
            var testCasesPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Cases\\0.random");
            Assert.True(Directory.Exists(testCasesPath), "Папка с тестовыми файлами не найдена");
            _testCasesRandom = reader.ReadCases(testCasesPath);
            
            testCasesPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Cases\\1.digits");
            _testCasesDigits = reader.ReadCases(testCasesPath);
            
            testCasesPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Cases\\2.sorted");
            _testCasesSorted = reader.ReadCases(testCasesPath); 
            
            testCasesPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Cases\\3.revers");
            _testCasesRevers = reader.ReadCases(testCasesPath);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}