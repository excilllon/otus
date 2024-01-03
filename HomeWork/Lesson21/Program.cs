using Lesson21;

var adjacent = new[]
{
    new [] { new[] { 1, 1 }, new[] { 4, 3 }, new[] { 5, 1 }},
    new [] { new[] { 0, 1 }, new[] { 2, 2 }, new[] { 4, 2 }},
    new [] { new[] { 1, 2 }, new[] { 3, 4 }, new[] { 4, 1 }},
    new [] { new[] { 2, 4 },new[] { 4, 3 }, new[] { 6, 1 }},
    new [] { new[] { 0, 3 }, new[] { 1, 2 }, new[] { 2, 1 }, new[] { 3, 3 }, new[] { 5, 2 }, new[] { 6, 4 }},
    new [] { new[] { 0, 1 },new[] { 4, 2 }, new[] { 6, 3 }},
    new [] { new[] { 3, 1 },new[] { 4, 4 }, new[] { 5, 3 }},
};
var graph = new Graph(adjacent, 7);
var dist = graph.ShortPathByDijkstra(0);
for (var vertex = 0; vertex < dist.Length; vertex++)
{
    Console.WriteLine($"Вершина: {vertex}, Расстояние: {dist[vertex]}");
}

Console.ReadKey();