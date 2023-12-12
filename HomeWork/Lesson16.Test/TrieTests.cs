namespace Lesson16.Test
{
    public class TrieTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TrieTest()
        {
            var trie = new Trie();
            trie.Insert("apple");
            Assert.True(trie.Search("apple"));  
            Assert.False(trie.Search("app"));     
            Assert.True(trie.StartsWith("app")); 
            trie.Insert("app");
            Assert.True(trie.Search("app"));     
        }
        
        [Test]
        public void TrieDictionaryTest()
        {
            var trieDictionary = new TrieDictionary<int>();
            trieDictionary.Put("apple", 150);
            Assert.AreEqual(trieDictionary.Get("apple"), 150);  
            trieDictionary.Put("apple", 233);
            Assert.AreEqual(trieDictionary.Get("apple"), 233); 
            trieDictionary.Put("app", 158);
            trieDictionary.Delete("apple");
            Assert.AreEqual(trieDictionary.Get("app"), 158);    
            Assert.AreEqual(trieDictionary.Get("apple"), 0); 
            trieDictionary.Put("car", 9644);
            trieDictionary.Put("car", 758);
            trieDictionary.Put("dog", 1244);
            trieDictionary.Put("dog1", 4777);
            trieDictionary.Put("dog2", 12);
            Assert.AreEqual(trieDictionary.Get("dog2"), 12); 
            Assert.AreEqual(trieDictionary.Get("dog1"), 4777); 
            Assert.AreEqual(trieDictionary.Get("dog"), 1244); 
            Assert.AreEqual(trieDictionary.Get("car"), 758); 
            trieDictionary.Delete("dog2");
            Assert.AreEqual(trieDictionary.Get("dog2"), 0); 
            Assert.AreEqual(trieDictionary.Get("dog1"), 4777); 
        }
    }
}