using Domain.Models;

namespace Domain.Abstract.Service
{
	public interface IMasterValidationService : IBaseService<MasterValidation>
	{
		Task<int> ChangeStatus(MasterValidation model);	}
}