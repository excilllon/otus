namespace Lesson10.Test
{
    public class Tests
    {
        [Test]
        public void BSTreeTest()
        {
            BSTree bsTree = new BSTree();
            bsTree.Insert(10);
            bsTree.Insert(8);
            bsTree.Insert(100);
            bsTree.Insert(56);
            bsTree.Insert(766);
            bsTree.Insert(62);
            bsTree.Insert(444);
            Assert.True(bsTree.Search(8));
            Assert.True(bsTree.Search(100));
            Assert.True(bsTree.Search(10));
            Assert.True(bsTree.Search(444));
            Assert.True(bsTree.Search(766));
            Assert.False(bsTree.Search(765));
            bsTree.Remove(100);
            bsTree.Remove(766);
            bsTree.Remove(62);
            bsTree.Remove(7888);
            Assert.False(bsTree.Search(62));
            Assert.False(bsTree.Search(766));
            bsTree.Insert(100);
            Assert.True(bsTree.Search(100));
        }

        [Test]
        public void AVLTreeTest()
        {
            var avlTree = new AVLTree();
            avlTree.Insert(10);
            avlTree.Insert(8);
            avlTree.Insert(100);
            avlTree.Insert(56);
            avlTree.Insert(-56);
            avlTree.Insert(-43);
            avlTree.Insert(-5);
            avlTree.Insert(7);
            avlTree.Insert(766);
            avlTree.Insert(62);
            avlTree.Insert(444);
            Assert.True(avlTree.Search(8));
            Assert.True(avlTree.Search(100));
            Assert.True(avlTree.Search(10));
            Assert.True(avlTree.Search(444));
            Assert.True(avlTree.Search(766));
            Assert.True(avlTree.Search(-56));
            Assert.True(avlTree.Search(-43));
            Assert.True(avlTree.Search(766));
            Assert.False(avlTree.Search(765));
            avlTree.Remove(100);
            avlTree.Remove(766);
            avlTree.Remove(62);
            avlTree.Remove(7888);
            Assert.False(avlTree.Search(62));
            Assert.False(avlTree.Search(766));
            avlTree.Insert(100);
            Assert.True(avlTree.Search(100));
        }
    }
}