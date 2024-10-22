using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface IF02Repository : IBaseRepository<F02>
    {
        Task<List<F02>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formTransactionID);
    }

}