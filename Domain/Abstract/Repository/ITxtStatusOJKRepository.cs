using Domain.Models;
using System.Data;

namespace Domain.Abstract.Repository
{
    public interface ITxtStatusOJKRepository : IBaseRepository<TxtStatusOJK>
    {
        Task<List<TxtStatusOJK>> GetTop(IDbTransaction transaction, int limit);
    }

}