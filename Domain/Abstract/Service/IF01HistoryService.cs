using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface IF01HistoryService : IBaseService<F01History>
    {
        Task<List<F01History>> GetRowsForLookup(string keyword, int offset, int limit, string? formTransactionId);
        Task<List<F01History>> GetRows(string keyword, int offset, int limit, string formTransactionHistoryID);
    }
}
