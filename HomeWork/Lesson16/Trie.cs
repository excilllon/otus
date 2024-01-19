namespace Lesson16
{
    /// <summary>
    /// Префиксное дерево
    /// </summary>
    public  sealed class Trie
    {
        private const int AlphabetSize = 128;
        private TrieNode _root = new TrieNode();
        
        public void Insert(string word)
        {
            TrieNode node = _root;
            foreach (var letter in word)
            {
                node[letter] ??= new TrieNode();
                node = node[letter];
            }
            node.IsWordEnd = true;
        }
    
        public bool Search(string word)
        {
            var foundNode = FindNodes(word);
            return foundNode?.IsWordEnd == true;
        }

        public bool StartsWith(string prefix) 
        {
            return FindNodes(prefix) != null;
        }

        private TrieNode? FindNodes(string word)
        {
            var node = _root;
            foreach (var letter in word)
            {
                if (node[letter] == null) return null;
                node = node[letter];
            }

            return node;
        }

        private class TrieNode
        {
            public TrieNode[] Childs { get; set; } = new TrieNode[AlphabetSize];
            public bool IsWordEnd { get; set; }

            public TrieNode? this[char index]
            {
                get => Childs[index];
                set => Childs[index] = value;
            }
        }
    }
}
