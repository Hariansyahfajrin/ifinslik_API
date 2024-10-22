using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface IF06HistoryRepository : IBaseRepository<F06History>
    {
        Task<List<F06History>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formTransactionHistoryID);
    }

}