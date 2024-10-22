using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface IFormTransactionHistoryService  : IBaseService<FormTransactionHistory>
    {
          Task<List<FormTransactionHistory>> GetRows(string keyword, int offset, int limit, string formType);
    }
}
