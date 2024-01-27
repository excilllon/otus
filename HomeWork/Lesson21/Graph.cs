namespace Lesson21
{
    /// <summary>
    /// Граф заданный вектром смежности с указанием весов ребер
    /// </summary>
    public sealed class Graph
    {
        /// <summary>
        /// 1й индекс - номер вершины, 2й индекс - номер смежной вершины, 3й индекс - стоимость ребра между этими верщинами
        /// </summary>
        private readonly int[][][] _adjVec;

        private readonly int _verticesCount;

        public Graph(int[][][] adjVec, int verticesCount)
        {
            _adjVec = adjVec;
            _verticesCount = verticesCount;
        }

        /// <summary>
        /// Поиск кратчайших путей от заданной вершины по агоритму Дейкстры
        /// </summary>
        /// <returns>Массив стоимостей пути до верщин</returns>
        public int[] ShortPathByDijkstra(int fromVertex)
        {
            var distances = new int[_verticesCount];

            for (var vertex = 0; vertex < distances.Length; vertex++)
            {
                distances[vertex] = int.MaxValue;
            }

            distances[fromVertex] = 0;
            PriorityQueue queue = new PriorityQueue(_verticesCount);
            queue.Enqueue(fromVertex, 0);

            while (!queue.Empty())
            {
                var nextVertex = queue.Dequeue();

                foreach(var adjVertex in _adjVec[nextVertex.value])
                {
                    var newDistance = distances[nextVertex.value] + adjVertex[1];
                    if (newDistance >= distances[adjVertex[0]]) continue;

                    distances[adjVertex[0]] = newDistance;
                    queue.Enqueue(adjVertex[0], newDistance);
                }
            }

            return distances;
        }
    }
}
