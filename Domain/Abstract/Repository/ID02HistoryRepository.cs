using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface ID02HistoryRepository : IBaseRepository<D02History>
    {
        Task<List<D02History>> GetRowsForLookup(IDbTransaction transaction, string keyword, int offset, int limit, string? period, string? financeCompanyType);
        Task<List<D02History>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formTransactionHistoryID);
    }

}