using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface IK01Service : IBaseService<K01>
    {
        Task<List<K01>> GetRows(string keyword, int offset, int limit, string formTransactionID);
    }
}
