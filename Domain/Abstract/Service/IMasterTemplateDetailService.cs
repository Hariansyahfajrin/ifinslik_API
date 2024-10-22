using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface IMasterTemplateDetailService : IBaseService<MasterTemplateDetail>
    {
        Task<int> ChangeStatus(MasterTemplateDetail masterTemplateDetail);
        Task<List<MasterTemplateDetail>> GetRows(string keyword, int offset, int limit, string masterTemplateID);
    }
}
