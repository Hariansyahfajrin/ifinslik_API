using System.Data;
using Domain.Models;

namespace Domain.Abstract.Repository
{
	public interface IMasterFaqRepository : IBaseRepository<MasterFaq>
	{
		Task<int> ChangeStatus(IDbTransaction transaction, MasterFaq model);
	}
}
