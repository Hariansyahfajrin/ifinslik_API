using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface IBuktiBerhasilLaporRepository : IBaseRepository<BuktiBerhasilLapor>
    {
        Task<int> ChangeStatus(IDbTransaction transaction, BuktiBerhasilLapor buktiBerhasilLapor);
        Task<List<BuktiBerhasilLapor>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string status);
        Task<List<BuktiBerhasilLapor>> GetRowsForLookup(IDbTransaction transaction, string keyword, int offset, int limit);
        Task<List<BuktiBerhasilLapor>> GetTop(IDbTransaction transaction, int limit);

    }

}