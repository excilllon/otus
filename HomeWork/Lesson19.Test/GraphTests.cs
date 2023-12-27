namespace Lesson19.Test
{
    public class GraphTests
    {
        [Test]
        public void TopologicalSortDemukron()
        {
            // Пример графа из лекции
            var adjMatrix = new []
            {
                new []
                {
                    0, 1, 0, 0, 0, 0, 0, 0, 0, 0
                },
                new []
                {
                    0, 0, 0, 0, 1, 0, 0, 0, 0, 0
                },
                new []
                {
                    0, 0, 0, 1, 0, 0, 0, 0, 0, 0
                },
                new []
                {
                    1, 1, 0, 0, 1, 1, 0, 0, 0, 0
                },
                new []
                {
                    0, 0, 0, 0, 0, 0, 1, 0, 0, 0
                },
                new []
                {
                    0, 0, 0, 0, 1, 0, 0, 1, 0, 0
                },
                new []
                {
                    0, 0, 0, 0, 0, 0, 0, 1, 0, 0
                },
                new []
                {
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                },
                new []
                {
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 1
                },
                new []
                {
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                }
            };
            var graph = new Graph(adjMatrix, 10);
            var result = graph.TopologicalSort();
            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(2, result[0][0]);
            Assert.AreEqual(8, result[0][1]);
            Assert.AreEqual(3, result[1][0]);
            Assert.AreEqual(9, result[1][1]);
            Assert.AreEqual(0, result[2][0]);
            Assert.AreEqual(5, result[2][1]);
            Assert.AreEqual(1, result[3][0]);
            Assert.AreEqual(4, result[4][0]);
            Assert.AreEqual(6, result[5][0]);
            Assert.AreEqual(7, result[6][0]);
        }
    }
}