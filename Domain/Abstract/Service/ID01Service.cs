using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface ID01Service : IBaseService<D01>
    {
        Task<List<D01>> GetRowsForLookup(string keyword, int offset, int limit, string? formTransactionId);
        Task<List<D01>> GetRows(string keyword, int offset, int limit, string formTransactionID);
    }
}
