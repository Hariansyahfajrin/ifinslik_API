using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface IF01Service : IBaseService<F01>
    {
        Task<List<F01>> GetRowsForLookup(string keyword, int offset, int limit, string? formTransactionId);
        Task<List<F01>> GetRows(string keyword, int offset, int limit, string formTransactionID);
    }
}
