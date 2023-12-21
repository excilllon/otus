namespace Lesson18
{
    /// <summary>
    /// Граф
    /// </summary>
    public sealed class Graph
    {
        private readonly int[][] _adjVec;
        private readonly VerticesList[] _reversedAdjVec;

        public Graph(int[][] adjVec)
        {
            _adjVec = adjVec;
            _reversedAdjVec = ReverseGraph();
        }
        /// <summary>
        /// Поиск в глубину по исходному графу
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="visited"></param>
        /// <param name="vertOrder"></param>
        private void Dfs1(int vertex, bool[] visited, Stack vertOrder)
        {
            visited[vertex] = true;
            foreach (var adjacentVertex in _adjVec[vertex])
            {
                if (!visited[adjacentVertex])
                    Dfs1(adjacentVertex, visited, vertOrder);
            }
            vertOrder.Push(vertex);
        }
        /// <summary>
        /// Поиск в глубину по инвертированному графу
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="components"></param>
        /// <param name="index"></param>
        private void Dfs2(int vertex, int[] components, int index)
        {
            components[vertex] = index;
            var adjacentVertex = _reversedAdjVec[vertex];
            while (adjacentVertex != null)
            {
                if (components[adjacentVertex.Value] < 0)
                    Dfs2(adjacentVertex.Value, components, index);
                adjacentVertex = adjacentVertex.Next;
            }
        }

        /// <summary>
        /// Поиск компонентов сильной связанности по алгоритму Косарайю
        /// </summary>
        /// <returns>Возвращает массив вершин с номером компонента в значении по индексу вершины</returns>
        public int[] FindSCbyKosaraju()
        {
            if (_adjVec.Length == 0) return new int[0];
            var orderStack = new Stack();
            var visited = new bool[_adjVec.Length];
            for (int i = 0; i < _adjVec.Length; i++)
            {
                if (!visited[i])
                    Dfs1(i, visited, orderStack);
            }
            var order = orderStack.Pop();
            var components = new int[_adjVec.Length];
            Array.Fill(components, -1);
            int componentIndex = 0;
            while (order != null)
            {
                if(components[order.Value] < 0) Dfs2(order.Value, components, componentIndex++);
                order = orderStack.Pop();
            }
            return components;
        }

        /// <summary>
        /// Инвертировать ребра графа
        /// </summary>
        /// <returns></returns>
        private VerticesList?[] ReverseGraph()
        {
            var reversedVec = new VerticesList?[_adjVec.Length];
            for (int i = 0; i < _adjVec.Length; i++)
            {
                foreach (var vertex in _adjVec[i])
                {
                    if (reversedVec[vertex] == null) reversedVec[vertex] = new VerticesList() { Value = i };
                    else reversedVec[vertex].Push(i);
                }
            }
            return reversedVec;
        }

        /// <summary>
        /// список смежных вершин
        /// </summary>
        private class VerticesList
        {
            public int Value { get; set; }
            public VerticesList? Next { get; private set; }
            public void Push(int value)
            {
                if (Next == null)
                {
                    Next = new VerticesList() { Value = value };
                    return;
                }
                Next.Next = new VerticesList() { Value = value };
            }
        }

        /// <summary>
        /// Стэк для обратного порядка обхода
        /// </summary>
        private class Stack
        {
            private int _value;
            private Stack? _next;
            private Stack? _top { get; set; }
            public void Push(int value)
            {
                if (_top == null)
                {
                    _top = new Stack() { _value = value };
                    return;
                }

                var oldHead = _top;
                _top = new Stack() { _value = value, _next = oldHead };
            }

            public int? Pop()
            {
                if (_top == null) return null;
                var value = _top._value;
                _top = _top._next;
                return value;
            }
        }
    }
}
