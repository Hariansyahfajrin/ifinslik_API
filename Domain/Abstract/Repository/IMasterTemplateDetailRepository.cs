using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface IMasterTemplateDetailRepository : IBaseRepository<MasterTemplateDetail>
    {
        Task<int> ChangeStatus(IDbTransaction transaction, MasterTemplateDetail masterTemplateDetail);
        Task<List<MasterTemplateDetail>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string masterTemplateID);

    }

}