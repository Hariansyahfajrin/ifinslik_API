using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface IM01HistoryRepository : IBaseRepository<M01History>
    {
        Task<List<M01History>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formTransactionHistoryID);
    }

}