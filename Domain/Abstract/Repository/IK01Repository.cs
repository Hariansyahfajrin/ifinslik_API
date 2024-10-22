using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface IK01Repository : IBaseRepository<K01>
    {
        Task<List<K01>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formTransactionID);
    }

}