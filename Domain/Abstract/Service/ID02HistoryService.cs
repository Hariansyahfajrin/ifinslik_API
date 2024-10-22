using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface ID02HistoryService : IBaseService<D02History>
    {
        Task<List<D02History>> GetRowsForLookup(string keyword, int offset, int limit, string? formTransactionId);
        Task<List<D02History>> GetRows(string keyword, int offset, int limit, string formTransactionHistoryID);
    }
}
