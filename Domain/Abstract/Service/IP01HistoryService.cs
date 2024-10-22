using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface IP01HistoryService : IBaseService<P01History>
    {
        Task<List<P01History>> GetRows(string keyword, int offset, int limit, string formTransactionHistoryID);
    }
}
