using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface IMasterTemplateService  : IBaseService<MasterTemplate>
    {
           Task<int>ChangeStatus(MasterTemplate masterTemplate);
           
    }
}
