using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface ID01HistoryRepository : IBaseRepository<D01History>
    {
        Task<List<D01History>> GetRowsForLookup(IDbTransaction transaction, string keyword, int offset, int limit, string? period, string? financeCompanyType);
        Task<List<D01History>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formTransactionHistoryID);
    }

}