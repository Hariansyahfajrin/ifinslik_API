using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface IF06HistoryService : IBaseService<F06History>
    {
        Task<List<F06History>> GetRows(string keyword, int offset, int limit, string formTransactionHistoryID);
    }
}
