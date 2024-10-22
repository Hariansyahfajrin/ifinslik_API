using System.Transactions;
using Domain.Abstract.Repository;
using Domain.Abstract.Service;
using Domain.Models;
using Service.Helper;

namespace Service
{


  public class BuktiBerhasilLaporService(IBuktiBerhasilLaporRepository repo) : BaseService, IBuktiBerhasilLaporService
  {
    private readonly IBuktiBerhasilLaporRepository _repo = repo;

    public async Task<List<BuktiBerhasilLapor>> GetRows(string keyword, int offset, int limit, string status)
    {
      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      try
      {
        var result = await _repo.GetRows(transaction, keyword, offset, limit, status);
        transaction.Commit();

        return result;
      }
      catch (Exception)
      {
        transaction.Rollback();
        throw;
      }


    }
    public Task<List<BuktiBerhasilLapor>> GetRows(string keyword, int offset, int limit)
    {
      throw new NotImplementedException();
    }

    public async Task<BuktiBerhasilLapor> GetRowByID(string id)
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

    public async Task<int> Insert(BuktiBerhasilLapor model)
    {

      if (model.CompanyCode == null)
      {
        model.CompanyCode = "COMP";
      }
      if (model.IsActive == null)
      {
        model.IsActive = 1;
      }
      if (model.IsBackup == null)
      {
        model.IsBackup = 0;
      }
      if (model.PeriodePelaporan == null)
      {
        model.PeriodePelaporan = model.Year + model.Month;
      }
      if (model.FileName == null)
      {
        model.FileName = "";
      }
      if (model.Paths == null)
      {
        model.Paths = "";
      }

      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();


      var lastRowList = await _repo.GetTop(transaction, 1);
      model.Code = GenerateCode(new FormatCode
      {
        Prefix = $"BBL",
        Date = DateTime.Now,
        RunNumberLen = 4,
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

    public async Task<int> UpdateByID(BuktiBerhasilLapor model)
    {
      {
        model.PeriodePelaporan = model.Year + model.Month;
      }
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
    public async Task<int> ChangeStatus(BuktiBerhasilLapor buktiBerhasilLapor)
    {
      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      try
      {
        var result = await _repo.ChangeStatus(transaction, buktiBerhasilLapor);
        transaction.Commit();
        return result;
      }
      catch (Exception)
      {
        transaction.Rollback();
        throw;
      }

    }
    public async Task<List<BuktiBerhasilLapor>> GetRowsForLookup(string keyword, int offset, int limit)
    {
      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      try
      {
        var result = await _repo.GetRowsForLookup(transaction, keyword, offset, limit);
        transaction.Commit();
        return result;
      }
      catch (Exception)
      {
        transaction.Rollback();
        throw;
      }


    }

  }

}
