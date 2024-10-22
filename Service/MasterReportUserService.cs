using System.Data;
using Domain.Abstract.Repository;
using Domain.Abstract.Service;
using Domain.Models;
using Service.Helper;

namespace Service
{
  public class MasterReportUserService : BaseService, IMasterReportUserService
  {
    private readonly IMasterReportUserRepository _repo;

    public MasterReportUserService(IMasterReportUserRepository repo)
    {
      _repo = repo;
    }

    #region GetRows
    public async Task<List<MasterReportUser>> GetRows(string? keyword, int offset, int limit)
    {

      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      try
      {
        List<MasterReportUser> result = await _repo.GetRows(transaction, keyword, offset, limit);

        transaction.Commit();
        return result;
      }
      catch (Exception)
      {
        transaction.Rollback();
        throw;
      }

    }

    #endregion

    #region GetRowByID
    public async Task<MasterReportUser> GetRowByID(string id)
    {

      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      try
      {
        MasterReportUser result = await _repo.GetRowByID(transaction, id);

        transaction.Commit();
        return result;
      }
      catch (Exception)
      {
        transaction.Rollback();
        throw;
      }

    }

    #endregion

    #region Insert
    public async Task<int> Insert(MasterReportUser model)
    {

      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      try
      {
        int result = await _repo.Insert(transaction, model);

        transaction.Commit();
        return result;
      }
      catch (Exception)
      {
        transaction.Rollback();
        throw;
      }

    }
    #endregion

    #region UpdateByID
    public async Task<int> UpdateByID(MasterReportUser model)
    {

      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      try
      {
        int result = await _repo.UpdateByID(transaction, model);

        transaction.Commit();
        return result;
      }
      catch (Exception)
      {
        transaction.Rollback();
        throw;
      }

    }
    #endregion

    #region DeleteByID
    public async Task<int> DeleteByID(string[] ids)
    {

      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      try
      {
        int result = 0;
        foreach (string id in ids)
        {
          result += await _repo.DeleteByID(transaction, id);
        }

        transaction.Commit();
        return result;
      }
      catch (Exception)
      {
        transaction.Rollback();
        throw;
      }

    }
    #endregion

  }
}
