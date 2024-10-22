using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface IA01Repository : IBaseRepository<A01>
    {
        Task<List<A01>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formTransactionID);
    }

}