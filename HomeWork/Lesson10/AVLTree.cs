using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson10
{
    /// <summary>
    /// Реализация АВЛ-дерева
    /// </summary>
    public class AVLTree : BSTree
    {
        //protected override TreeNode? Remove(TreeNode? node, int x)
        //{
            
        //    if (node == null) return node;

        //    if (node.Key > x)
        //        node.Left = Remove(node.Left, x);
            

        //    if (node.Key < x)
        //        node.Right = Remove(node.Right, x);

        //    if (node.Left == null || node.Right == null)
        //    {
        //        node = node.Right ?? node.Left;
        //    }

        //    TreeNode nodeParent = node;
        //    var curNode = node.Right;
        //    while (curNode.Left != null)
        //    {
        //        nodeParent = curNode;
        //        curNode = curNode.Left;
        //    }

        //    if (nodeParent != node)
        //    {
        //        nodeParent.Left = curNode.Right;
        //    }
        //    else
        //    {
        //        nodeParent.Right = curNode.Right;
        //    }
        //    node.Key = curNode.Key;
        //}

        protected override TreeNode Insert(TreeNode? node, int x)
        {
            if (node == null) return new TreeNode() { Key = x };
            if (node.Key < x)
                node.Right = Insert(node.Right, x);
            if (node.Key > x)
                node.Left = Insert(node.Left, x);
            else return node;

            node.Height = 1 + Math.Max(node.Left?.Height ?? 0, node.Right?.Height ?? 0);
            return Rebalance(node, x);
        }

        private TreeNode Rebalance(TreeNode node, int x)
        {
            var balance = node?.Left?.Height ?? 0 - node?.Right?.Height ?? 0;
            return balance switch
            {
                > 1 when x < node.Left.Key => SmallRightRotation(node),
                < -1 when x > node.Right.Key => SmallLeftRotation(node),
                > 1 when x > node.Left.Key => BigRightRotation(node),
                < -1 when x < node.Right.Key => BigLeftRotation(node),
                _ => node
            };
        }

        /// <summary>
        /// Большой левый поворот
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private TreeNode BigLeftRotation(TreeNode node)
        {
            node.Right = SmallRightRotation(node.Right);
            return SmallLeftRotation(node);
        }

        /// <summary>
        /// Большой правый поворот
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private TreeNode BigRightRotation(TreeNode node)
        {
            node.Left = SmallLeftRotation(node.Left);
            return SmallRightRotation(node);
        }

        /// <summary>
        /// Малый поворот заданного узла направо
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private TreeNode SmallRightRotation(TreeNode node)
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
        /// Малый поворот заданного узла налево
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private TreeNode SmallLeftRotation(TreeNode node)
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
