namespace Lesson16
{
    /// <summary>
    /// Ассоциативный массив на основе префиксного дерева
    /// </summary>
    /// <typeparam name="V"></typeparam>
    public sealed class TrieDictionary<V>
    {
        private const int AlphabetSize = 128;
        private TrieNode<V> _root = new TrieNode<V>();

        public void Put(string key, V value)
        {
            TrieNode<V> node = _root;
            foreach (var letter in key)
            {
                node[letter] ??= new TrieNode<V>();
                node = node[letter];
            }
            node.Value = value;
            node.IsValueNode = true;
        }

        public V Get(string key)
        {
            var node = _root;
            foreach (var letter in key)
            {
                if (node[letter] == null) return default;
                node = node[letter];
            }
            return node.IsValueNode ? node.Value : default;
        }

        public void Delete(string key)
        {
            TrieNode<V> node = _root;
            foreach (var letter in key)
            {
                if (node[letter] == null) return;
                node = node[letter];
            }

            node.Value = default;
            node.IsValueNode = false;
        }

        private class TrieNode<V>
        {
            public TrieNode<V>[] Childs { get; set; } = new TrieNode<V>[AlphabetSize];
            public bool IsValueNode { get; set; }
            public V Value { get; set; }
            public TrieNode<V>? this[char index]
            {
                get => Childs[index];
                set => Childs[index] = value;
            }
        }
    }
}
