using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface IMasterOJKReferenceService  : IBaseService<MasterOJKReference>
    {
      Task<int>ChangeStatus(MasterOJKReference masterOJKReference);

      Task<int>ChangeStatusBySubCode(SysGeneralSubCode sysGeneralSubCode);
      Task<List<MasterOJKReference>> GetRows(string keyword, int offset, int limit, string sysGeneralSubCodeID);
      Task<List<MasterOJKReference>> GetRowsForLookup(string keyword, int offset, int limit, string? code);

    }
}
