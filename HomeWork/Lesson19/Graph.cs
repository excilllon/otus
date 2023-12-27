namespace Lesson19
{
    /// <summary>
    /// Орграф заданный матрицей смежности
    /// </summary>
    public sealed class Graph
    {
        private readonly int[][] _adjVec;
        private readonly int _sMax;

        public Graph(int[][] adjVec, int sMax)
        {
            _adjVec = adjVec;
            _sMax = sMax;
        }

        /// <summary>
        /// Топологическая сортировка по алгоритму Демукрона
        /// </summary>
        /// <returns>Массив уровней с номерами вершин на каждом уровне</returns>
        public int[][] TopologicalSort()
        {
            // Сумма полустепеней по вершинам
            int[] halfDegrees = new int[_sMax];
            var vertexQueue = new Queue();

            for (int i = 0; i < _sMax; i++)
            {
                for (int j = 0; j < _sMax; j++)
                {
                    halfDegrees[i] += _adjVec[j][i];
                }
                // очередь для вычитания полустепеней вершин из полученной суммы
                if (halfDegrees[i] == 0)
                    vertexQueue.Enqueue(i, 0);
            }

            var sortedVertices = new Queue();
            var lastLevel = 0;
            while (vertexQueue.Any())
            {
                var zerDegreeVertex = vertexQueue.Dequeue();
                for (var i = 0; i < _adjVec[zerDegreeVertex.Value].Length; i++)
                {
                    // если уже был 0, ставим -1, чтобы не считать повторно
                    halfDegrees[i] = halfDegrees[i] <= 0 ? -1 : halfDegrees[i] - _adjVec[zerDegreeVertex.Value][i];
                    if (halfDegrees[i] == 0)
                    {
                        lastLevel = zerDegreeVertex.Level + 1;
                        vertexQueue.Enqueue(i, lastLevel);
                    }
                }
                // очередь отсортированных вершин
                sortedVertices.Enqueue(zerDegreeVertex.Value, zerDegreeVertex.Level);
            }

            // запись в массив указанного в задании формата
            var levels = new int[lastLevel + 1][];
            int k = 0;
            while (sortedVertices.Any())
            {
                var vertex = sortedVertices.Dequeue();
                if (levels[vertex.Level] == null)
                {
                    levels[vertex.Level] = new int[_sMax];
                    Array.Fill(levels[vertex.Level], -1);
                    k = 0;
                }
                levels[vertex.Level][k++] = vertex.Value;
            }

            return levels;
        }

        private class Queue
        {
            private QueueElement? _top;
            private QueueElement? _last;

            public void Enqueue(int vertex, int level)
            {
                if (_top == null)
                {
                    _top = new QueueElement() { Value = vertex, Level = level };
                    _last = _top;
                    return;
                }
                _last.Next = new QueueElement() { Value = vertex, Level = level };
                _last = _last.Next;
            }

            public QueueElement? Dequeue()
            {
                if (_top == null) return null;
                var oldTop = _top;
                _top = _top.Next;
                return oldTop;
            }

            public bool Any()
            {
                return _top != null;
            }
        }

        private class QueueElement
        {
            public int Value { get; set; }
            public int Level { get; set; }
            public QueueElement Next { get; set; }
        }
    }
}
