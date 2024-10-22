using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface IM01Repository : IBaseRepository<M01>
    {
        Task<List<M01>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formTransactionID);
    }

}