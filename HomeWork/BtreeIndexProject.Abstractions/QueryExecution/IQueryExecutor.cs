using BtreeIndexProject.Model.QueryExecution;
using BtreeIndexProject.Model.ViewModels;

namespace BtreeIndexProject.Abstractions.QueryExecution
{
    public interface IQueryExecutor
    {
        Task<QueryResult> Execute(QueryModelInput model);
    }
}
