using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
	public interface ISysGeneralSubCodeRepository : IBaseRepository<SysGeneralSubCode>
	{
		Task<int> ChangeStatus(IDbTransaction transaction, SysGeneralSubCode SysGeneralSubCode);

		Task<List<SysGeneralSubCode>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string sysGeneralCodeID);
		Task<List<SysGeneralSubCode>> GetRowsForLookup(IDbTransaction transaction, string keyword, int offset, int limit, string? code);
		Task<SysGeneralSubCode> GetRowByCode(IDbTransaction transaction, string? code);

	}

}