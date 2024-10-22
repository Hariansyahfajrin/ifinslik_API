using System.Data;
using Domain.Abstract.Repository;
using Domain.Abstract.Service;
using Domain.Models;
using Service.Helper;

namespace Service
{
  public class P01HistoryService : BaseService, IP01HistoryService
  {
    private readonly IP01HistoryRepository _repo;

    public P01HistoryService(IP01HistoryRepository repo)
    {
      _repo = repo;
    }

    #region GetRows

    public async Task<List<P01History>> GetRows(string keyword, int offset, int limit, string formTransactionHistoryID)
    {
      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      try
      {
        var result = await _repo.GetRows(transaction, keyword, offset, limit, formTransactionHistoryID);
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
    public async Task<P01History> GetRowByID(string id)
    {

      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      try
      {
        P01History result = await _repo.GetRowByID(transaction, id);

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
    public async Task<int> Insert(P01History model)
    {

      if (model.FlagDetail == null)
      {
        model.FlagDetail = "D";
      }
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
    public async Task<int> UpdateByID(P01History model)
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

    public Task<List<P01History>> GetRows(string keyword, int offset, int limit)
    {
      throw new NotImplementedException();
    }
    #endregion

  }
}
