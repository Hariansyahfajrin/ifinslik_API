using Domain.Models;

namespace Domain.Abstract.Service
{
	public interface ISysGeneralSubCodeService : IBaseService<SysGeneralSubCode>
	{
		Task<int> ChangeStatus(SysGeneralSubCode sysGeneralSubCode);
		Task<List<SysGeneralSubCode>> GetRows(string keyword, int offset, int limit, string sysGeneralCodeID);
		Task<List<SysGeneralSubCode>> GetRowsForLookup(string keyword, int offset, int limit, string? code);
		Task<SysGeneralSubCode> GetRowByCode(string? code);
	}
}
