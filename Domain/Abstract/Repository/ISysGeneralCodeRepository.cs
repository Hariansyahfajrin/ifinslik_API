using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface ISysGeneralCodeRepository  : IBaseRepository<SysGeneralCode>
    {
            Task<int>ChangeStatus(IDbTransaction transaction, SysGeneralCode SysGeneralCode);
    } 

}