using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface IA01Service : IBaseService<A01>
    {
        Task<List<A01>> GetRows(string keyword, int offset, int limit, string formTransactionID);
    }
}
