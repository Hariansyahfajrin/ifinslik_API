using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface IF02HistoryService : IBaseService<F02History>
    {
        Task<List<F02History>> GetRows(string keyword, int offset, int limit, string formTransactionHistoryID);
    }
}
