using Domain.Abstract.Repository;
using Domain.Abstract.Service;
using Domain.Models;
using Service.Helper;

namespace Service
{


  public class SysGeneralSubCodeService(ISysGeneralSubCodeRepository repo, IMasterOJKReferenceRepository subCodeRepo) : BaseService, ISysGeneralSubCodeService
  {
    private readonly ISysGeneralSubCodeRepository _repo = repo;

    private readonly IMasterOJKReferenceRepository _subCodeRepo = subCodeRepo;

    public async Task<List<SysGeneralSubCode>> GetRows(string keyword, int offset, int limit, string sysGeneralCodeID)
    {
      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      try
      {
        var result = await _repo.GetRows(transaction, keyword, offset, limit, sysGeneralCodeID);
        transaction.Commit();

        return result;
      }
      catch (Exception)
      {
        transaction.Rollback();
        throw;
      }

    }

    public Task<List<SysGeneralSubCode>> GetRows(string keyword, int offset, int limit)
    {
      throw new NotImplementedException();
    }

    public async Task<SysGeneralSubCode> GetRowByID(string id)
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
    public async Task<SysGeneralSubCode> GetRowByCode(string? code)
    {
      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      try
      {
        var result = await _repo.GetRowByCode(transaction, code);
        transaction.Commit();

        return result;
      }
      catch (Exception)
      {
        transaction.Rollback();
        throw;
      }

    }
    public async Task<int> Insert(SysGeneralSubCode model)
    {
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
    public async Task<int> UpdateByID(SysGeneralSubCode model)
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

    public async Task<int> ChangeStatus(SysGeneralSubCode sysGeneralSubCode)
    {
      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      try
      {
        int result = await _repo.ChangeStatus(transaction, sysGeneralSubCode);

        if (result > 0)
        {
          var data = await _repo.GetRowByID(transaction, sysGeneralSubCode.ID ?? "");

          sysGeneralSubCode.IsActive = data.IsActive;

          result += await _subCodeRepo.ChangeStatusBySubCode(transaction, sysGeneralSubCode);
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
    public async Task<List<SysGeneralSubCode>> GetRowsForLookup(string keyword, int offset, int limit, string? code)
    {
      using var connection = _repo.GetDbConnection();
      using var transaction = connection.BeginTransaction();

      try
      {
        var result = await _repo.GetRowsForLookup(transaction, keyword, offset, limit, code);
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
