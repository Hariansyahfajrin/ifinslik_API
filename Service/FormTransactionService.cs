using System.Transactions;
using Domain.Abstract.Repository;
using Domain.Abstract.Service;
using Domain.Models;
using Service.Helper;

namespace Service
{


  public class FormTransactionService(IFormTransactionRepository repo) : BaseService, IFormTransactionService
  {
    private readonly IFormTransactionRepository _repo = repo;

    public async Task<List<FormTransaction>> GetRows(string keyword, int offset, int limit, string formType, DateTime? date)
    {
      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      try
      {
        string dateString = date.HasValue ? date.Value.ToString("yyyy/MM/dd") : null;
        var result = await _repo.GetRows(transaction, keyword, offset, limit, formType, (DateTime)date);
        transaction.Commit();
        return result;
      }
      catch (Exception)
      {
        transaction.Rollback();
        throw;
      }


    }

    public async Task<List<FormTransaction>> GetRows(string keyword, int offset, int limit)
    {
      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      try
      {
        var result = await _repo.GetRows(transaction, keyword, offset, limit);
        transaction.Commit();

        return result;
      }
      catch (Exception)
      {
        transaction.Rollback();
        throw;
      }


    }

    public async Task<FormTransaction> GetRowByID(string id)
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

    public async Task<int> Insert(FormTransaction model)
    {
      if (model.CompanyCode == null)
      {
        model.CompanyCode = "COMP";
      }

      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      var lastRowList = await _repo.GetTop(transaction, 1);
      model.Code = GenerateCode(new FormatCode
      {
        Prefix = $"FTR",
        Date = DateTime.Now,
        RunNumberLen = 6,
        Delimiter = ".",

      }, lastRowList.FirstOrDefault(), "Code");
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
    public async Task<int> UpdateByID(FormTransaction model)
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

    public Task<List<FormTransaction>> GetRows(string keyword, int offset, int limit, string formType, DateTime date)
    {
      throw new NotImplementedException();
    }
  }

}
