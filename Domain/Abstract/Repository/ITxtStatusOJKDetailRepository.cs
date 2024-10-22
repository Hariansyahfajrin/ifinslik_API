using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface ITxtStatusOJKDetailRepository : IBaseRepository<TxtStatusOJKDetail>
    {

        Task<List<TxtStatusOJKDetail>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string txtStatusOJKID);
    }

}