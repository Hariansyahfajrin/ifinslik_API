using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface IF06Service : IBaseService<F06>
    {
        Task<List<F06>> GetRows(string keyword, int offset, int limit, string formTransactionID);
    }
}
