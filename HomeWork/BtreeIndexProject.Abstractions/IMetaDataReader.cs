namespace BtreeIndexProject.Abstractions;

public interface IMetaDataReader
{
	Task ReadMetaData(string dbFolder);
}