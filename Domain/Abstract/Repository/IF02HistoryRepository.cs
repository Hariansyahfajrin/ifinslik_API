using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface IF02HistoryRepository : IBaseRepository<F02History>
    {
        Task<List<F02History>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formTransactionHistoryID);
    }

}