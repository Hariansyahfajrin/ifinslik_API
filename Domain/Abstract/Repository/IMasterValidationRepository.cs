using System.Data;
using Domain.Models;

namespace Domain.Abstract.Repository
{
	public interface IMasterValidationRepository : IBaseRepository<MasterValidation>
	{
		Task<int> ChangeStatus(IDbTransaction transaction, MasterValidation model);
	}
}
