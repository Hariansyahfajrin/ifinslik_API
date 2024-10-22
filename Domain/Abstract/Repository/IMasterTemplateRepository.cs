using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface IMasterTemplateRepository : IBaseRepository<MasterTemplate>
    {
        Task<int> ChangeStatus(IDbTransaction transaction, MasterTemplate masterTemplate);
        Task<List<MasterTemplate>> GetTop(IDbTransaction transaction, int limit);

    }

}