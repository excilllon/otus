namespace BtreeIndexProject.Model.MetaData;

public class ColumnMetaData
{
    public string Name { get; set; }
    public ColumnType Type { get; set; }
    public bool IsPrimaryKey { get; set; }
    public int Order { get; set; }
}