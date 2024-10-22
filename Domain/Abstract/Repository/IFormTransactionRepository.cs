using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface IFormTransactionRepository : IBaseRepository<FormTransaction>
    {
        Task<List<FormTransaction>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string formType, DateTime? date);
        Task<List<FormTransaction>> GetTop(IDbTransaction transaction, int limit);
    }

}