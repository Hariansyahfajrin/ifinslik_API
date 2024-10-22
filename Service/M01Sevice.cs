using System.Transactions;
using Domain.Abstract.Repository;
using Domain.Abstract.Service;
using Domain.Models;
using Service.Helper;

namespace Service
{


  public class M01Service(IM01Repository repo, IFormTransactionRepository transactionRepo) : BaseService, IM01Service
  {
    private readonly IM01Repository _repo = repo;
    private readonly IFormTransactionRepository _transactionRepo = transactionRepo;

    public async Task<List<M01>> GetRows(string keyword, int offset, int limit, string formTransactionID)
    {
      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      try
      {
        var result = await _repo.GetRows(transaction, keyword, offset, limit, formTransactionID);
        transaction.Commit();

        return result;
      }
      catch (Exception)
      {
        transaction.Rollback();
        throw;
      }


    }

    public async Task<M01> GetRowByID(string id)
    {
      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      try
      {
        var result = await _repo.GetRowByID(transaction, id);
        transaction.Commit();

        return result;
      }
      catch (Exception)
      {
        transaction.Rollback();
        throw;
      }

    }

    public async Task<int> Insert(M01 model)
    {
      if (model.FlagDetail == null)
      {
        model.FlagDetail = "D";
      }
      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      try
      {
        var formTransaction = await _transactionRepo.GetRowByID(transaction, model.FormTransactionID
        ?? throw new Exception(""));

        model.Period = formTransaction.PeriodePelaporan;

        var result = await _repo.Insert(transaction, model);
        transaction.Commit();
        return result;
      }
      catch (Exception)
      {
        transaction.Rollback();
        throw;
      }

    }
    public async Task<int> UpdateByID(M01 model)
    {
      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      try
      {
        var result = await _repo.UpdateByID(transaction, model);
        transaction.Commit();
        return result;
      }
      catch (Exception)
      {
        transaction.Rollback();
        throw;
      }

    }

    public async Task<int> DeleteByID(string[] idList)
    {
      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      try
      {
        var result = 0;
        foreach (var id in idList)
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

    public Task<List<M01>> GetRows(string keyword, int offset, int limit)
    {
      throw new NotImplementedException();
    }
  }

}
