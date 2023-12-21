namespace Lesson18.Test
{
    public class Tests
    {
        [Test]
        public void TestKosaraju()
        {
            var adjacent = new []
            {
                new[] { 1, 4 }, new[] { 2 }, new[] { 3 }, new[] { 1 }, new[] { 3 }
            };
            Graph graph = new Graph(adjacent);
            var components = graph.FindSCbyKosaraju();
            Assert.AreEqual(5, components.Length);
            Assert.AreEqual(0, components[0]);
            Assert.AreEqual(2, components[1]);
            Assert.AreEqual(1, components[4]);
        }
    }
}