namespace BtreeIndexProject.Abstractions
{
	public interface IRowOffsetStorage
	{
		Task InitOffsets();
		long GetOffset(long idValue);
	}
}
