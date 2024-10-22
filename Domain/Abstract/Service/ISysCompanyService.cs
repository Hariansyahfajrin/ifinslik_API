using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface ISysCompanyService  : IBaseService<SysCompany>
    {
        Task<List<SysCompany>> GetRowsForLookup(string keyword, int offset, int limit);
    }
}
