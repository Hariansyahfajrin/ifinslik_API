using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface ID02Repository : IBaseRepository<D02>
    {
        Task<List<D02>> GetRowsForLookup(IDbTransaction transaction, string keyword, int offset, int limit, string? period, string? financeCompanyType);
        Task<List<D02>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formTransactionID);
    }

}