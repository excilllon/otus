
namespace BtreeIndexProject.Services.BTreeFile
{
	/// <summary>
	/// узел B-дерева (файловая реализация)
	/// </summary>
	internal class BTreeFileNode
	{
		public BTreeFileNode(int minDegree, bool isLeaf)
		{
			IsLeaf = isLeaf;
			Keys = new (int key, long offset)[2 * minDegree - 1];
			Childs = new int[2 * minDegree];
		}
		/// <summary>
		/// Текущее количество ключей в узле
		/// </summary>
		public int CurrentKeysCount { get; set; } 
		/// <summary>
		/// Массив ключ-смещение
		/// </summary>
		public (int key, long offset)[] Keys { get; set; }
		/// <summary>
		/// Номера 
		/// </summary>
		public int[] Childs { get; set; }
		/// <summary>
		/// Является ли узел листом
		/// </summary>
		public bool IsLeaf { get; set; }
		/// <summary>
		/// Номер блока в файле, где хранится узел
		/// </summary>
		public int BlockNumber { get; set; } = -1;
	}
}
