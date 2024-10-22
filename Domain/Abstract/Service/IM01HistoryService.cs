using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface IM01HistoryService : IBaseService<M01History>
    {
        Task<List<M01History>> GetRows(string keyword, int offset, int limit, string formTransactionHistoryID);
    }
}
