// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using BenchmarkDotNet.Running;
using Lesson4.Arrays;
using Lesson4.Benchmarks;

//IArray<int> array = new MatrixArray<int>(5);
//array.Add(0, 0);
//array.Add(10, 1);
//array.Add(4, 2);
//array.Add(4, 3);
//array.Add(78, 4);
//array.Add(-96, 5);
//array.Print();
//array.Add(33, 2);
//array.Add(1455, 2);
//array.Add(9784, 2);
//array.Add(23, 2);
//array.Add(335, 2);
//array.Add(15, 2);
//array.Print();
//array.Add(156, 0);
//array.Add(999, 999);
//array.Print();
//array.Remove(0);
//array.Print();
//array.Remove(999);
//array.Print();
//array.Remove(4);
//array.Print();

//var queue = new PriorityQueue<string>();
//queue.Enqueue(0, "hh");
//queue.Enqueue(0, "bb");
//Console.WriteLine(queue.Dequeue());
//queue.Enqueue(10, "aa");
//queue.Enqueue(110, "са");
//queue.Enqueue(6, "oi");
//Console.WriteLine(queue.Dequeue());
//Console.WriteLine(queue.Dequeue());
//queue.Enqueue(0, "bb");
//Console.WriteLine(queue.Dequeue());
//Console.WriteLine(queue.Dequeue());
//Console.WriteLine(queue.Dequeue());
//Console.ReadKey();

BenchmarkRunner.Run<ArrayAddBenchmark>();
BenchmarkRunner.Run<ArrayRemoveBenchmark>();
//int N = 100000;
//var data = new int[N];
//var random = new Random(42);
//MatrixArray<int> singleArray = new(10);
//Stopwatch watch = new Stopwatch();

//watch.Start();
//for (int i = 0; i < N; i++)
//{
//    singleArray.Add(random.Next(), 0);
//}
//watch.Stop();
//Console.WriteLine(watch.ElapsedMilliseconds);
//watch.Reset();
//watch.Start();
//for (var i = 0; i < N; i++)
//{
//    singleArray.Remove(0);
//}
//watch.Stop();
//Console.WriteLine(watch.ElapsedMilliseconds);

Console.ReadKey();