using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface IK01HistoryRepository : IBaseRepository<K01History>
    {
        Task<List<K01History>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formTransactionHistoryID);
    }

}