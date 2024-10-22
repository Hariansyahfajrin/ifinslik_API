using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface IP01Service : IBaseService<P01>
    {
        Task<List<P01>> GetRows(string keyword, int offset, int limit, string formTransactionID);
    }
}
