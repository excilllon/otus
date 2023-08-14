using System.Text.RegularExpressions;

namespace TestCaseReader
{
    /// <summary>
    /// Чтение файлов с кейсами
    /// </summary>
    public class CaseReader
    {
        public IEnumerable<TestCase> ReadCases(string path)
        {
            if (!Directory.Exists(path)) yield break;
            
            var testFiles = Directory.EnumerateFiles(path)
                .Where(f => Path.HasExtension(".in") || Path.HasExtension(".out"))
                .GroupBy(f => Path.GetFileName(f[..f.LastIndexOf('.')]));
            foreach (var testCaseFile in testFiles)
            {
                var inFileName = testCaseFile.FirstOrDefault();
                var expectedFileName = testCaseFile.LastOrDefault();
                if (inFileName == null || expectedFileName == null) continue;

                yield return new TestCase()
                {
                    In = File.ReadAllText(inFileName).Trim(),
                    Expected = File.ReadAllText(expectedFileName).Trim(),
                    TestCaseNumber = testCaseFile.Key
                };
            }
        }
    }
}