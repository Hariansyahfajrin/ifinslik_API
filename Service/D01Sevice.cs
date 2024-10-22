using System.Transactions;
using Domain.Abstract.Repository;
using Domain.Abstract.Service;
using Domain.Models;
using Service.Helper;

namespace Service
{


  public class D01Service(ID01Repository repo, IFormTransactionRepository transactionRepo) : BaseService, ID01Service
  {
    private readonly ID01Repository _repo = repo;
    private readonly IFormTransactionRepository _transactionRepo = transactionRepo;

    public async Task<List<D01>> GetRows(string keyword, int offset, int limit, string formTransactionID)
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

    public async Task<D01> GetRowByID(string id)
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

    public async Task<int> Insert(D01 model)
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
    public async Task<int> UpdateByID(D01 model)
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
    public async Task<List<D01>> GetRowsForLookup(string keyword, int offset, int limit, string? formTransactionId)
    {
      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      try
      {
        var formTransaction = await _transactionRepo.GetRowByID(transaction, formTransactionId ?? "");

        var result = await _repo.GetRowsForLookup(transaction, keyword, offset, limit, formTransaction.Date?.ToString("yyyyMM"), formTransaction.FinanceCompanyType);
        transaction.Commit();
        return result;
      }
      catch (Exception)
      {
        transaction.Rollback();
        throw;
      }


    }

    public Task<List<D01>> GetRows(string keyword, int offset, int limit)
    {
      throw new NotImplementedException();
    }
  }

}