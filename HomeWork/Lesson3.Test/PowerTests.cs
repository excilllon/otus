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
            Assert.True(Directory.Exists(testCasesPath), "����� � ��������� ������� �� �������");
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
                        $"�������� ������ {testCase.TestCaseNumber} �� �������, ���������: {expected}, ����: {actual}");
                    TestContext.Out.WriteLine(
                        $"������: ����: {testCase.TestCaseNumber}, ���������: {expected}, ����: {actual}");
                }
                else Assert.Fail($"�������� ���� {testCase.TestCaseNumber} �� ������� ����������");
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