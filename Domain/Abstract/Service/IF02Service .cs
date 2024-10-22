using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface IF02Service : IBaseService<F02>
    {
        Task<List<F02>> GetRows(string keyword, int offset, int limit, string formTransactionID);
    }
}
