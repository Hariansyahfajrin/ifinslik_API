using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface IBuktiBerhasilLaporService  : IBaseService<BuktiBerhasilLapor>
    {
           Task<int>ChangeStatus(BuktiBerhasilLapor buktiBerhasilLapor);
           Task<List<BuktiBerhasilLapor>> GetRows(string keyword, int offset, int limit, string status);
           Task<List<BuktiBerhasilLapor>> GetRowsForLookup(string keyword, int offset, int limit);
    }
}
