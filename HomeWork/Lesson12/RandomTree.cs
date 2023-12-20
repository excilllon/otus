namespace Lesson12
{
    /// <summary>
    /// Рандомизированное дерево 
    /// </summary>
    public sealed class RandomTree : SplayTree
    {
        private Random _random = new Random(42);

        /// <summary>
        /// Рандомизированный поиск.
        /// </summary>
        /// <param name="key"></param>
        public override bool Search(int key)
        {
            if (_root == null) return false;

            if (_random.Next() % (_root.Height) == 0)
                return base.Search(key);
            return Search(_root, key) != null;
        }

        private TreeNode? Search(TreeNode? node, int x)
        {
            if (node == null) return null;
            if (node.Key > x)
                return Search(node.Left, x);
            if (node.Key < x)
                return Search(node.Right, x);
            return node;
        }

        /// <summary>
        /// Рандомизированная вставка. В зависимости от случайного значения
        /// вставляемый ключ будет поднят в корень либо будет произведена обычная вставка
        /// </summary>
        /// <param name="key"></param>
        public override void Insert(int key)
        {
            if (_root == null)
            {
                _root = new TreeNode() { Key = key };
                return;
            }

            if (_random.Next() % (_root.Height + 1) == 0)
                InsertRoot(key);
            else Insert(_root, key);
        }

        /// <summary>
        /// "Простая" вставка в двоичное дерево
        /// </summary>
        /// <param name="node"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        private TreeNode Insert(TreeNode? node, int x)
        {
            if (node == null) return new TreeNode() { Key = x };
            if (node.Key < x)
                node.Right = Insert(node.Right, x);
            if (node.Key > x)
                node.Left = Insert(node.Left, x);
            node.Height = 1 + Math.Max(node.Left?.Height ?? 0, node.Right?.Height ?? 0);
            return node;
        }

        /// <summary>
        /// Рандомизированное удаление. В зависимости от случайного значения
        /// в корень будет поднято значение либо из левого узла от удаляемого либо из правого
        /// </summary>
        /// <param name="key"></param>
        public override void Remove(int key)
        {
            if (_root == null)
            {
                return;
            }

            _root = Splay(_root, key);
            if (_root.Key != key) return;

            if (_root.Left == null || _root.Right == null)
            {
                _root = _root.Right ?? _root.Left;
            }
            else
            {
                var oldRoot = _root;
                var left = _root.Left;
                var right = _root.Right;
                if (_random.Next() % (left.Height + right.Height) < left.Height)
                {
                    _root = Splay(_root.Left, key);
                    _root.Right = oldRoot.Right;
                }
                else
                {
                    _root = Splay(_root.Right, key);
                    _root.Left = oldRoot.Left;
                }
            }
            if (_root != null)
                _root.Height = Math.Max(_root.Left?.Height ?? 0, _root.Right?.Height ?? 0) + 1;
        }
    }
}
