using System.Transactions;
using Domain.Abstract.Repository;
using Domain.Abstract.Service;
using Domain.Models;
using Service.Helper;

namespace Service
{


  public class F01HistoryService(IF01HistoryRepository repo, IFormTransactionHistoryRepository transactionRepo) : BaseService, IF01HistoryService
  {
    private readonly IF01HistoryRepository _repo = repo;
    private readonly IFormTransactionHistoryRepository _transactionRepo = transactionRepo;

    public async Task<List<F01History>> GetRows(string keyword, int offset, int limit, string formTransactionHistoryID)
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

    public async Task<F01History> GetRowByID(string id)
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

    public async Task<int> Insert(F01History model)
    {
      if (model.FlagDetail == null)
      {
        model.FlagDetail = "D";
      }
      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      try
      {
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
    public async Task<int> UpdateByID(F01History model)
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
    public async Task<List<F01History>> GetRowsForLookup(string keyword, int offset, int limit, string? formTransactionId)
    {
      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      try
      {
        var formTransaction = await _transactionRepo.GetRowByID(transaction, formTransactionId ?? "");

        var result = await _repo.GetRowsForLookup(transaction, keyword, offset, limit, formTransaction.Date?.ToString("yyyyMM"), formTransaction.CompanyCode, formTransaction.FinanceCompanyType);
        transaction.Commit();
        return result;
      }
      catch (Exception)
      {
        transaction.Rollback();
        throw;
      }


    }

    public Task<List<F01History>> GetRows(string keyword, int offset, int limit)
    {
      throw new NotImplementedException();
    }
  }

}
