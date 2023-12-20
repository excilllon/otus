namespace Lesson12.Test
{
    public class TreeTests
    {
        [Test]
        public void SplayTreeTest()
        {
            var splayTree = new SplayTree();
            splayTree.Insert(10);
            splayTree.Insert(8);
            splayTree.Insert(100);
            splayTree.Insert(56);
            splayTree.Insert(766);
            splayTree.Insert(62);
            splayTree.Insert(444);
            Assert.True(splayTree.Search(8));
            Assert.True(splayTree.Search(100));
            Assert.True(splayTree.Search(10));
            Assert.True(splayTree.Search(444));
            Assert.True(splayTree.Search(766));
            Assert.False(splayTree.Search(765));
            splayTree.Remove(100);
            splayTree.Remove(766);
            splayTree.Remove(62);
            splayTree.Remove(7888);
            Assert.False(splayTree.Search(62));
            Assert.False(splayTree.Search(766));
            splayTree.Insert(100);
            Assert.True(splayTree.Search(100));
        }
        
        [Test]
        public void RandomTree()
        {
            var randomTree = new RandomTree();
            randomTree.Insert(10);
            randomTree.Insert(8);
            randomTree.Insert(100);
            randomTree.Insert(56);
            randomTree.Insert(766);
            randomTree.Insert(62);
            randomTree.Insert(444);
            Assert.True(randomTree.Search(8));
            Assert.True(randomTree.Search(100));
            Assert.True(randomTree.Search(10));
            Assert.True(randomTree.Search(444));
            Assert.True(randomTree.Search(766));
            Assert.False(randomTree.Search(765));
            randomTree.Remove(100);
            randomTree.Remove(766);
            randomTree.Remove(62);
            randomTree.Remove(7888);
            Assert.False(randomTree.Search(62));
            Assert.False(randomTree.Search(766));
            randomTree.Insert(100);
            Assert.True(randomTree.Search(100));
        }
    }
}