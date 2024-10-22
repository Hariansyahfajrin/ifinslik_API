using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface IP01HistoryRepository : IBaseRepository<P01History>
    {
        Task<List<P01History>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formTransactionHistoryID);
    }

}