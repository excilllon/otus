using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson10
{
    public class BSTree
    {
        protected TreeNode? _rootNode;

        public void Insert(int x)
        {
            _rootNode = Insert(_rootNode, x);
        }

        protected virtual TreeNode Insert(TreeNode? node, int x)
        {
            if (node == null) return new TreeNode() { Key = x };
            if (node.Key < x)
                node.Right = Insert(node.Right, x);
            if (node.Key > x)
                node.Left = Insert(node.Left, x);
            return node;
        }

        public bool Search(int x)
        {
            return Search(_rootNode, x) != null;
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

        public void Remove(int x)
        {
            Remove(_rootNode, x);
        }

        protected virtual TreeNode? Remove(TreeNode? node, int x)
        {
            if (node == null) return node;

            if (node.Key > x)
            {
                node.Left = Remove(node.Left, x);
                return node;
            }

            if (node.Key < x)
            {
                node.Right = Remove(node.Right, x);
                return node;
            }

            if (node.Left == null || node.Right == null)
            {
                return node.Right ?? node.Left;
            }

            TreeNode nodeParent = node;
            var curNode = node.Right;
            while (curNode.Left != null)
            {
                nodeParent = curNode;
                curNode = curNode.Left;
            }

            if (nodeParent != node)
            {
                nodeParent.Left = curNode.Right;
            }
            else
            {
                nodeParent.Right = curNode.Right;
            }
            node.Key = curNode.Key;
            return node;
        }
    }
}
