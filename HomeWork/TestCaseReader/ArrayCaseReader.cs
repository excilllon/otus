namespace TestCaseReader
{
    public class ArrayCaseReader
    {
        public IEnumerable<ArrayTestCase> ReadCases(string path, string type, char separator = ' ')
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
                var inContent = File.ReadAllLines(inFileName);
                yield return new ArrayTestCase()
                {
                    In = inContent[1].Trim().Split(separator).Select(v => int.Parse(v)).ToArray(),
                    Expected = File.ReadAllText(expectedFileName).Trim().Split(separator).Select(v => int.Parse(v)).ToArray(),
                    TestCaseNumber = testCaseFile.Key,
                    TestCaseType = type
                };
            }
        }
    }
}
