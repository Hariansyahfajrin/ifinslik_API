using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface IFormTransactionHistoryRepository : IBaseRepository<FormTransactionHistory>
    {
        Task<List<FormTransactionHistory>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formType);
        Task<List<FormTransactionHistory>> GetTop(IDbTransaction transaction, int limit);
    }

}