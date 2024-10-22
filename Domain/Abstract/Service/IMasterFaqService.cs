using Domain.Models;

namespace Domain.Abstract.Service
{
	public interface IMasterFaqService : IBaseService<MasterFaq>
	{
		Task<int> ChangeStatus(MasterFaq model);	}
}