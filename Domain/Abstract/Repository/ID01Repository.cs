using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface ID01Repository : IBaseRepository<D01>
    {
        Task<List<D01>> GetRowsForLookup(IDbTransaction transaction, string keyword, int offset, int limit, string? period, string? financeCompanyType);
        Task<List<D01>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formTransactionID);
    }

}