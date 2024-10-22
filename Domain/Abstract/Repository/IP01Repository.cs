using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface IP01Repository : IBaseRepository<P01>
    {
        Task<List<P01>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formTransactionID);
    }

}