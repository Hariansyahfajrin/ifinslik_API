using System.Data;
using Domain.Models;

namespace Domain.Abstract.Repository
{
    public interface ISysCompanyRepository  : IBaseRepository<SysCompany>
    {
       Task<List<SysCompany>> GetRowsForLookup(IDbTransaction transaction, string keyword, int offset, int limit);
    } 

}