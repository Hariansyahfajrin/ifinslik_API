using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface IA01HistoryService : IBaseService<A01History>
    {
        Task<List<A01History>> GetRows(string keyword, int offset, int limit, string formTransactionHistoryID);
    }
}
