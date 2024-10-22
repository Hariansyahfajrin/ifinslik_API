using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface ISysGeneralCodeService  : IBaseService<SysGeneralCode>
    {
      Task<int>ChangeStatus(SysGeneralCode sysGeneralCode);
    }
}
