using BtreeIndexProject.Services.BTree;

namespace BtreeIndexProject.Services.BTreeFile
{
	internal class BTreeFileNode
	{
		public BTreeFileNode(int t1, bool isLeaf)
		{
			MinimumDegree = t1;
			IsLeaf = isLeaf;
			Keys = new (int key, long offset)[2 * MinimumDegree - 1];
			Childs = new int[2 * MinimumDegree];
		}
		/// <summary>
		/// Минимальная степень дерева
		/// Каждый узел, кроме корня, содержит не менее MinimumDegree−1
		/// ключей, и каждый внутренний узел имеет по меньшей мере MinimumDegree  дочерних узлов. 
		/// </summary>
		public int MinimumDegree { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int CurrentKeysCount { get; set; } 
		public (int key, long offset)[] Keys { get; set; }
		public int[] Childs { get; set; }
		/// <summary>
		/// Является ли узел листом
		/// </summary>
		public bool IsLeaf { get; set; }

		public int BlockNumber { get; set; } = -1;
	}
}
