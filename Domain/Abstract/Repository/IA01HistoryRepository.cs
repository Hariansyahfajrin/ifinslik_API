using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface IA01HistoryRepository : IBaseRepository<A01History>
    {
        Task<List<A01History>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formTransactionHistoryID);
    }

}