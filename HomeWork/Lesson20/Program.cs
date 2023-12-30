using Lesson20;

var adjacent = new[]
{
    new [] { new[] { 1, 4 }, new[] { 7, 8 }},
    new [] { new[] { 2, 8 }, new[] { 7, 11 }},
    new [] { new[] { 5, 4 }, new[] { 3, 7 }},
    new [] { new[] { 4, 9 }, new[] { 5, 14 }},
    new [] { new int[0] },
    new [] { new[] { 4, 10 }},
    new [] { new[] { 5, 2 }},
    new [] { new[] { 6, 1 }, new[] { 8, 7 }},
    new [] { new[] { 2, 2 }, new[] { 6, 6 }},
};
var graph = new Graph(adjacent);
var edges = graph.MinSpanTreeByKruskal();
foreach (var edge in edges)
{
    Console.WriteLine($"Вершина: {edge.V1}, Вершина: {edge.V2}, Вес: {edge.Weight}");
}
Console.ReadKey();

