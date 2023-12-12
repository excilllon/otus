// See https://aka.ms/new-console-template for more information

using Lesson16;

Console.WriteLine("Hello, World!");
Trie trie = new Trie();
trie.Insert("apple");
trie.Search("apple");   // return True
trie.Search("app");     // return False
trie.StartsWith("app"); // return True
trie.Insert("app");
trie.Search("app");     //