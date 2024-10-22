using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface ISysGlobalParamRepository  : IBaseRepository<SysGlobalParam>
    {
            Task<int>ChangeEditableStatus(IDbTransaction transaction, SysGlobalParam sysGlobalParam);
    } 

}