namespace BtreeIndexProject.Model
{
	public sealed class DbTreeItem
	{
		public string Name { get; set; }
		public bool Expanded { get; set; } = true;
		public DbTreeItem[] Items { get; set; }
		public string icon { get; set; }
	}
}
