using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface IFormTransactionService  : IBaseService<FormTransaction>
    {
        Task<List<FormTransaction>> GetRows(string keyword, int offset, int limit, string formType,DateTime? date);
    }
}
