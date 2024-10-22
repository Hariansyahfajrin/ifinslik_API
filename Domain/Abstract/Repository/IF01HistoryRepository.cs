using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface IF01HistoryRepository : IBaseRepository<F01History>
    {
        Task<List<F01History>> GetRowsForLookup(IDbTransaction transaction, string keyword, int offset, int limit, string? period, string? companyCode, string? financeCompanyType);
        Task<List<F01History>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formTransactionHistoryID);
    }

}