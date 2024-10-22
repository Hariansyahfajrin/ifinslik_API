using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface IK01HistoryService : IBaseService<K01History>
    {
        Task<List<K01History>> GetRows(string keyword, int offset, int limit, string formTransactionHistoryID);
    }
}
