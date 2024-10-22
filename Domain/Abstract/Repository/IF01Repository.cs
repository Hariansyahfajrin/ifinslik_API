using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface IF01Repository : IBaseRepository<F01>
    {
        Task<List<F01>> GetRowsForLookup(IDbTransaction transaction, string keyword, int offset, int limit, string? period, string? companyCode, string? financeCompanyType);
        Task<List<F01>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formTransactionID);
    }

}