namespace BtreeIndexProject.Services.BTree
{
	internal class BTree<E>
		where E : IComparable
	{
		BTreeNode<E>? root;
		private int T;

		public BTree(int t)
		{
			T = t;
		}

		public BTreeNode<E>? Search(E k)
		{
			return root?.Search(k);
		}

		// The main function that inserts a new key in this B-Tree
		public void Insert(E k)
		{
			// If tree is empty
			if (root == null)
			{
				// Allocate memory for root
				root = new BTreeNode<E>(T, true);
				root.Keys[0] = k;  // Insert key
				root.CurrentKeysCount = 1;  // Update number of keys in root
			}
			else // If tree is not empty
			{
				// If root is full, then tree grows in height
				if (root.CurrentKeysCount == 2 * T - 1)
				{
					// Allocate memory for new root
					var s = new BTreeNode<E>(T, false);

					// Make old root as child of new root
					s.Childs[0] = root;

					// Split the old root and move 1 key to the new root
					s.SplitChild(0, root);

					// New root has two children now.  Decide which of the
					// two children is going to have new key
					int i = 0;
					if (s.Keys[0].CompareTo(k) < 0)
						i++;
					s.Childs[i].InsertNonFull(k);

					// Change root
					root = s;
				}
				else  // If root is not full, call insertNonFull for root
					root.InsertNonFull(k);
			}
		}
	}
}
