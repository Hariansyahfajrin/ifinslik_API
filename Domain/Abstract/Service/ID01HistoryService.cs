using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface ID01HistoryService : IBaseService<D01History>
    {
        Task<List<D01History>> GetRowsForLookup(string keyword, int offset, int limit, string? formTransactionId);
        Task<List<D01History>> GetRows(string keyword, int offset, int limit, string formTransactionHistoryID);
    }
}
