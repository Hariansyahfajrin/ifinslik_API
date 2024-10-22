using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface ID02Service : IBaseService<D02>
    {
        Task<List<D02>> GetRowsForLookup(string keyword, int offset, int limit, string? formTransactionId);
        Task<List<D02>> GetRows(string keyword, int offset, int limit, string formTransactionID);
    }
}
