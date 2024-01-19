using BtreeIndexProject.Model;

namespace BtreeIndexProject.Abstractions
{
	public interface IQueryExecutor
	{
		Task<QueryResult> Execute(QueryModelInput model);
	}
}
