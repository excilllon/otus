using BtreeIndexProject.Model.MetaData;

namespace BtreeIndexProject.Services.MetaData;

internal class TableMetaData
{
    public string Name { get; set; }
    public List<ColumnMetaData> Columns { get; set; } = new();
    public List<TableIndex> Indicies { get; set; } = new();
}