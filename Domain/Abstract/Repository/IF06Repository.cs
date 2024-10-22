using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface IF06Repository : IBaseRepository<F06>
    {
        Task<List<F06>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formTransactionID);
    }

}