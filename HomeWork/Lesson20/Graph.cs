namespace Lesson20
{
    /// <summary>
    /// граф заданный вектром смежности с указанием весов ребер
    /// </summary>
    public sealed class Graph
    {
        /// <summary>
        /// 1й индекс - номер вершины, 2й индекс - номер смежной вершины, 3й индекс - стоимость ребра между этими верщинами
        /// </summary>
        private readonly int[][][] _adjVec;
        private readonly Edge[] _edges;
        private readonly int _sMax;

        public Graph(int[][][] adjVec)
        {
            _adjVec = adjVec;
            _sMax = adjVec.Length;
            if (_adjVec == null) return;

            var edgeCount = 0;
            for (var vertex = 0; vertex < adjVec.Length; vertex++)
            {
                if(adjVec[vertex].Length == 0 || adjVec[vertex][0].Length == 0) continue;
                edgeCount += adjVec[vertex].Length;
            }

            _edges = new Edge[edgeCount];
            int e = 0;
            // конветируем вектор смежности в массив ребер с весами
            for (var vertex = 0; vertex < adjVec.Length; vertex++)
            {
                var vertAdj = adjVec[vertex];
                foreach (var adjVertex in vertAdj)
                { 
                    if(adjVertex.Length == 0) continue;
                    _edges[e++] = new Edge()
                    {
                        V1 = vertex,
                        V2 = adjVertex[0],
                        Weight = adjVertex[1]
                    };
                }
            }
            // сортируем ребра по их весу, так как их нужно добавлять в MST, начиная с самых дешевых 
            Sortings.HeapSort(_edges);
        }

        /// <summary>
        /// Поиск минимально связанного графа по алгоритму Крускала
        /// </summary>
        /// <returns>Массив ребер</returns>
        public Edge[] MinSpanTreeByKruskal()
        {
            var results = new Edge[_sMax - 1];

            var subsets = new SubSet[_sMax];
            for (var i = 0; i < _sMax; i++)
                subsets[i] = new SubSet(i);

            var edgeIdx = 0;
            var newEdgeIdx = 0;

            while (newEdgeIdx < _sMax - 1)
            {
                var nextEdge = _edges[edgeIdx++];

                int srcVertexParent = FindSubsetParent(subsets, nextEdge.V1);
                int destVertexParent = FindSubsetParent(subsets, nextEdge.V2);

                // Проверяем если ли цикл
                if (srcVertexParent == destVertexParent) continue;

                results[newEdgeIdx++] = nextEdge;

                // Объединяем подмножества в одно
                UnionSubsets(subsets, srcVertexParent, destVertexParent);
            }
            return results;
        }

        private int FindSubsetParent(SubSet[] subsets, int vertex) 
        {
            if (subsets[vertex].Parent != vertex) 
                subsets[vertex].Parent = FindSubsetParent(subsets, subsets[vertex].Parent); 
  
            return subsets[vertex].Parent; 
        } 

        private void UnionSubsets(SubSet[] subsets, int srcVertex, int destVertex) 
        { 
            var srcVertexParent = FindSubsetParent(subsets, srcVertex); 
            var destVertexParent = FindSubsetParent(subsets, destVertex); 
  
            if (subsets[srcVertexParent].Rank < subsets[destVertexParent].Rank) 
                subsets[srcVertexParent].Parent = destVertexParent; 
            else if (subsets[srcVertexParent].Rank > subsets[destVertexParent].Rank) 
                subsets[destVertexParent].Parent = srcVertexParent;
            else { 
                subsets[destVertexParent].Parent = srcVertexParent; 
                subsets[srcVertexParent].Rank++; 
            } 
        } 

        /// <summary>
        /// Подмножество вершин
        /// </summary>
        private class SubSet
        {
            public SubSet(int v)
            {
                Parent = v;
            }
            public int Parent { get; set; }
            public int Rank { get; set; }
        }
    }
}
