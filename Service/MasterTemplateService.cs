using System.Transactions;
using Domain.Abstract.Repository;
using Domain.Abstract.Service;
using Domain.Models;
using Service.Helper;

namespace Service
{


  public class MasterTemplateService(IMasterTemplateRepository repo) : BaseService, IMasterTemplateService
  {
    private readonly IMasterTemplateRepository _repo = repo;

    public async Task<List<MasterTemplate>> GetRows(string keyword, int offset, int limit)
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

    public async Task<MasterTemplate> GetRowByID(string id)
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

    public async Task<int> Insert(MasterTemplate model)
    {
      if (model.DelimiterStart == null)
      {
        model.DelimiterStart = "0";
      }
      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      var lastRowList = await _repo.GetTop(transaction, 1);
      model.Code = GenerateCode(new FormatCode
      {
        Prefix = $"FTR",
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
    public async Task<int> UpdateByID(MasterTemplate model)
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
    public async Task<int> ChangeStatus(MasterTemplate masterTemplate)
    {
      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      try
      {
        var result = await _repo.ChangeStatus(transaction, masterTemplate);
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
