using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface IM01Service : IBaseService<M01>
    {
        Task<List<M01>> GetRows(string keyword, int offset, int limit, string formTransactionID);
    }
}
