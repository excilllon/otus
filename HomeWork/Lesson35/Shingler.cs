using System.Text;

namespace Lesson35
{
    /// <summary>
    /// Создает шинглы по заданному тексту
    /// </summary>
    internal class Shingler
    {
        private readonly int _k;
        private readonly char[] _separators = new []{' ', ';', '.', ':', '-'};
        public Shingler(int k)
        {
            _k = k;
        }
        
        public List<string> GetShingles(string text)
        {
            if (string.IsNullOrEmpty(text)) return new List<string>();
            
            var words = text.Split(_separators);
            if(words.Length < _k) return new List<string>(words);
            var shigles = new List<string>();
            for (var i = 0; i < words.Length - _k; i++)
            {
                var sb = new StringBuilder();
                for (var j = 0; j < _k; j++)
                {
                    sb.Append(words[i+j]);
                    sb.Append(' ');
                }
                shigles.Add(sb.ToString().Trim());
            }
            return shigles;
        }
    }
}
