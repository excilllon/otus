namespace Lesson12
{
    /// <summary>
    /// Splay-дерево
    /// </summary>
    public class SplayTree
    {
        protected TreeNode? _root;

        /// <summary>
        /// Поиск ключа в дереве
        /// </summary>
        /// <param name="key"></param>
        /// <returns>true-если найден</returns>
        public virtual bool Search(int key)
        {
            _root = Splay(_root, key);
            return _root?.Key == key;
        }

        /// <summary>
        /// Вставка ключа
        /// </summary>
        /// <param name="key"></param>

        public virtual void Insert(int key)
        {
            if (_root == null)
            {
                _root = new TreeNode() { Key = key };
                return;
            }

            InsertRoot(key);
        }

        /// <summary>
        /// Вставка с переносом ключа в корень
        /// </summary>
        /// <param name="key"></param>
        protected void InsertRoot(int key)
        {
            // Делаем ближайший узел-лист корнем 
            _root = Splay(_root, key);

            // Если ключ уже существует, то возвращаем его
            if (_root.Key == key)
                return;

            var node = new TreeNode() { Key = key };
            if (_root.Key > key)
            {
                node.Right = _root;
                node.Left = _root.Left;
                _root.Left = null;
            }
            else
            {
                node.Left = _root;
                node.Right = _root.Right;
                _root.Right = null;
            }
            _root = node;
            _root.Height = Math.Max(_root.Left?.Height ?? 0, _root.Right?.Height ?? 0) + 1;
        }

        /// <summary>
        /// Удаление ключа с предварительным переносом его в корень
        /// </summary>
        /// <param name="key"></param>
        public virtual void Remove(int key)
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
                _root = Splay(_root.Left, key);
                _root.Right = oldRoot.Right;
            }
            if (_root != null)
                _root.Height = Math.Max(_root.Left?.Height ?? 0, _root.Right?.Height ?? 0) + 1;
        }

        /// <summary>
        /// Поднимает ключ в корень если он есть в дереве
        /// </summary>
        /// <param name="root"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        protected TreeNode? Splay(TreeNode? root, int key)
        {
            // Базовые случаи: root равен NULL или
            // ключ находится в корне
            if (root == null || root.Key == key)
                return root;

            // Ключ лежит в левом поддереве
            if (root.Key > key)
            {
                // Ключа нет в дереве, мы закончили
                if (root.Left == null) return root;

                // Zig-Zig (Левый-левый) 
                if (root.Left.Key > key)
                {
                    // Сначала рекурсивно поднимем
                    // ключ как корень left-left
                    root.Left.Left = Splay(root.Left.Left, key);

                    // Первый разворот для root, 
                    // второй разворот выполняется после else 
                    root = RightRotate(root);
                }
                else if (root.Left.Key < key) // Zig-Zag (Левый-правый) 
                {
                    // Сначала рекурсивно поднимаем
                    // ключ как корень left-right

                    root.Left.Right = Splay(root.Left.Right, key);

                    // Выполняем первый разворот для root.left
                    if (root.Left.Right != null)
                        root.Left = LeftRotate(root.Left);
                }

                // Выполняем второй разворот для корня
                return (root.Left == null) ? root : RightRotate(root);
            }
            else // Ключ находится в правом поддереве 
            {
                // Ключа нет в дереве, мы закончили
                if (root.Right == null) return root;

                // Zag-Zig (Правый-левый) 
                if (root.Right.Key > key)
                {
                    //Поднять ключ как корень right-left
                    root.Right.Left = Splay(root.Right.Left, key);

                    // Выполняем первый поворот для root.right
                    if (root.Right.Left != null)
                        root.Right = RightRotate(root.Right);
                }
                else if (root.Right.Key < key)// Zag-Zag (Правый-правый) 
                {
                    // Поднимаем ключ как корень 
                    // right-right и выполняем первый разворот
                    root.Right.Right = Splay(root.Right.Right, key);
                    root = LeftRotate(root);
                }

                // Выполняем второй разворот для root
                return (root.Right == null) ? root : LeftRotate(root);
            }
        }

        /// <summary>
        ///  Zig (Правый разворот) 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private TreeNode RightRotate(TreeNode node)
        {
            var left = node.Left;
            var leftRight = left?.Right;

            left.Right = node;
            node.Left = leftRight;
            node.Height = Math.Max(node.Left?.Height ?? 0, node.Right?.Height ?? 0) + 1;
            left.Height = Math.Max(left?.Left?.Height ?? 0, left?.Right?.Height ?? 0) + 1;

            return left;
        }

        /// <summary>
        /// Zag (Левый разворот) 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private TreeNode LeftRotate(TreeNode node)
        {
            var right = node.Right;
            var rightLeft = right?.Left;

            right.Left = node;
            node.Right = rightLeft;
            node.Height = Math.Max(node.Left?.Height ?? 0, node.Right?.Height ?? 0) + 1;
            right.Height = Math.Max(right?.Left?.Height ?? 0, right?.Right?.Height ?? 0) + 1;

            return right;
        }
    }
}
