using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface ISysGlobalParamService  : IBaseService<SysGlobalParam>
    {
      Task<int>ChangeEditableStatus(SysGlobalParam sysGlobalParam);
    }
}
