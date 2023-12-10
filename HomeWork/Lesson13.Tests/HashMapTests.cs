namespace Lesson13.Test
{
    public class HashMapTests
    {
        private HashMap<string, int> _hashMap = new HashMap<string, int>(10);
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestHashMap()
        {
            Assert.AreEqual(_hashMap.Capacity, 10);
            _hashMap["cat"] = 5;
            _hashMap["dog"] = 5;
            _hashMap["cat"] = 10;
            Assert.True(_hashMap.ContainsKey("cat"));
            Assert.AreEqual(_hashMap["cat"], 10);
            _hashMap["car"] = 15;
            _hashMap["yellow"] = 65;
            _hashMap["blue"] = 78;
            Assert.AreEqual(_hashMap["yellow"], 65);
            Assert.AreEqual(_hashMap["blue"], 78);
            _hashMap.Remove("ddd");
            _hashMap.Remove("car");
            Assert.False(_hashMap.ContainsKey("car"));
            _hashMap["cat2"] = 25;
            _hashMap["cat3"] = 35;
            _hashMap["cat4"] = 45;
            _hashMap["cat5"] = 55;
            _hashMap["cat6"] = 65;
            _hashMap["cat7"] = 75;
            _hashMap["cat8"] = 85;
            _hashMap["cat9"] = 95;
            _hashMap["cat9"] = 105;
            Assert.AreEqual(_hashMap["cat"], 10);
            Assert.AreEqual(_hashMap["yellow"], 65);
            Assert.AreEqual(_hashMap["blue"], 78);
            Assert.False(_hashMap.ContainsKey("car"));
            Assert.AreEqual(_hashMap.Capacity, 21);
        }
    }
}