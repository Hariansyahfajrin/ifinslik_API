using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface IMasterOJKReferenceRepository  : IBaseRepository<MasterOJKReference>
    {
            Task<int>ChangeStatus(IDbTransaction transaction, MasterOJKReference masterOJKReference);
            Task<int>ChangeStatusBySubCode(IDbTransaction transaction, SysGeneralSubCode sysGeneralSubCode);
            Task<List<MasterOJKReference>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string sysGeneralSubCodeID);
            Task<List<MasterOJKReference>> GetRowsForLookup(IDbTransaction transaction, string keyword, int offset, int limit, string? code);

    } 

}