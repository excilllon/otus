namespace BtreeIndexProject.Services.BTree
{
	internal class BTreeNode<E>
		where E : IComparable
	{
		public BTreeNode(int t1, bool isLeaf)
		{
			MinimumDegree = t1;
			IsLeaf = isLeaf;
			Keys = new E[2 * MinimumDegree - 1];
			Childs = new BTreeNode<E>[2 * MinimumDegree];
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
		public E[] Keys { get; set; }
		public BTreeNode<E>[] Childs { get; set; }
		/// <summary>
		/// Является ли узел листом
		/// </summary>
		public bool IsLeaf { get; set; }

		// Function to search key k in subtree rooted with this node
		public BTreeNode<E> Search(E searchKey)
		{
			// Find the first key greater than or equal to k
			int i = 0;
			while (i < CurrentKeysCount && searchKey.CompareTo(Keys[i]) > 0)
				i++;

			// If the found key is equal to k, return this node
			if (Keys[i].Equals(searchKey)) return this;

			// If key is not found here and this is a leaf node
			if (IsLeaf) return null;

			// Go to the appropriate child
			return Childs[i].Search(searchKey);
		}

		// Function to traverse all nodes in a subtree rooted with this node
		public IEnumerable<E> Traverse()
		{
			// There are n keys and n+1 children, traverse through n keys
			// and first n children
			int i;
			var res = new List<E>();
			for (i = 0; i < CurrentKeysCount; i++)
			{
				// If this is not leaf, then before printing key[i],
				// traverse the subtree rooted with child C[i].
				if (!IsLeaf)
					return Childs[i].Traverse();
				res.Add(Keys[i]);
			}
			// Print the subtree rooted with last child
			if (!IsLeaf) return Childs[i].Traverse();
			return res;

		}

		// A utility function to insert a new key in this node
		// The assumption is, the node must be non-full when this
		// function is called
		public void InsertNonFull(E k)
		{
			// Initialize index as index of rightmost element
			int i = CurrentKeysCount - 1;

			// If this is a leaf node
			if (IsLeaf)
			{
				// The following loop does two things
				// a) Finds the location of new key to be inserted
				// b) Moves all greater keys to one place ahead
				while (i >= 0 && Keys[i].CompareTo(k) > 0)
				{
					Keys[i + 1] = Keys[i];
					i--;
				}

				// Insert the new key at found location
				Keys[i + 1] = k;
				CurrentKeysCount += 1;
			}
			else // If this node is not leaf
			{
				// Find the child which is going to have the new key
				while (i >= 0 && Keys[i].CompareTo(k) > 0)
					i--;

				// See if the found child is full
				if (Childs[i + 1].CurrentKeysCount == 2 * MinimumDegree - 1)
				{
					// If the child is full, then split it
					SplitChild(i + 1, Childs[i + 1]);

					// After split, the middle key of C[i] goes up and
					// C[i] is splitted into two.  See which of the two
					// is going to have the new key
					if (Keys[i + 1].CompareTo(k) < 0)
						i++;
				}
				Childs[i + 1].InsertNonFull(k);
			}
		}

		// A utility function to split the child y of this node
		// Note that y must be full when this function is called
		public void SplitChild(int i, BTreeNode<E> y)
		{
			// Create a new node which is going to store (t-1) keys
			// of y
			var z = new BTreeNode<E>(y.MinimumDegree, y.IsLeaf);
			z.CurrentKeysCount = MinimumDegree - 1;

			// Copy the last (t-1) keys of y to z
			for (int j = 0; j < MinimumDegree - 1; j++)
				z.Keys[j] = y.Keys[j + MinimumDegree];

			// Copy the last t children of y to z
			if (y.IsLeaf == false)
			{
				for (int j = 0; j < MinimumDegree; j++)
					z.Childs[j] = y.Childs[j + MinimumDegree];
			}

			// Reduce the number of keys in y
			y.CurrentKeysCount = MinimumDegree - 1;

			// Since this node is going to have a new child,
			// create space of new child
			for (int j = CurrentKeysCount; j >= i + 1; j--)
				Childs[j + 1] = Childs[j];

			// Link the new child to this node
			Childs[i + 1] = z;

			// A key of y will move to this node. Find the location of
			// new key and move all greater keys one space ahead
			for (int j = CurrentKeysCount - 1; j >= i; j--)
				Keys[j + 1] = Keys[j];

			// Copy the middle key of y to this node
			Keys[i] = y.Keys[MinimumDegree - 1];

			// Increment count of keys in this node
			CurrentKeysCount++;
		}
	}
}
