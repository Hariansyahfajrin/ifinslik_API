using Domain.Models;

namespace Domain.Abstract.Service
{
    public interface ITxtStatusOJKDetailService : IBaseService<TxtStatusOJKDetail>
    {
        Task<List<TxtStatusOJKDetail>> GetRows(string keyword, int offset, int limit, string txtStatusOJKID);
    }

}
