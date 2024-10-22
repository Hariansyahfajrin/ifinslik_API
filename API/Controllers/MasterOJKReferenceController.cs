using API.GeneralController;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Domain.Abstract.Service;

namespace API.Controllers
{
	[Route("/api/[controller]")]
	[ApiController]
	[SetBaseModelProperties]
	public class MasterOJKReferenceController(IMasterOJKReferenceService service, IConfiguration configuration) : BaseController(configuration)
	{
		private readonly IMasterOJKReferenceService _service = service;

		[HttpGet("GetRows")]
		public async Task<ActionResult> GetRows(string keyword, int offset, int limit,string sysGeneralSubCodeID)
		{
			try
			{
				var data = await _service.GetRows(keyword, offset, limit,sysGeneralSubCodeID);
				return ResponseSuccess(data);
			}
			catch (Exception ex)
			{
				return ResponseError(ex);
			}
		}

		[HttpGet("GetRowByID")]
		public async Task<ActionResult> GetRowByID(string id)
		{
			try
			{
				var data = await _service.GetRowByID(id);
				return ResponseSuccess(data);
			}
			catch (Exception ex)
			{
				return ResponseError(ex);
			}
		}

		[HttpPost("Insert")]
		public async Task<ActionResult> Insert(MasterOJKReference id)
		{
			try
			{
	
				return ResponseSuccess(new { id.ID }, await _service.Insert(id));
			}
			catch (Exception ex)
			{
				return ResponseError(ex);
			}
		}

		[HttpPut("UpdateByID")]
		public async Task<ActionResult> UpdateByID(MasterOJKReference category)
		{
			try
			{

				return ResponseSuccess(null, await _service.UpdateByID(category));
			}
			catch (Exception ex)
			{
				return ResponseError(ex);
			}
		}

		[HttpDelete("DeleteByID")]
		public async Task<ActionResult> DeleteByID([FromBody] string[] id)
		{
			try
			{
				return ResponseSuccess(null, await _service.DeleteByID(id));
			}
			catch (Exception ex)
			{
				return ResponseError(ex);
			}
		}
        [HttpPut("ChangeStatus")]
        public async Task<ActionResult> ChangeStatus(MasterOJKReference MasterOJKReference)
        {
            try
            {
				return ResponseSuccess(null, await _service.ChangeStatus(MasterOJKReference));
            }
            catch (Exception ex)
            {
                return ResponseError(ex);
            }
        }
			[HttpGet("GetRowsForLookup")]
		public async Task<ActionResult> GetRowsForLookup(string? keyword, int offset, int limit, string? code)
		{
			try
			{
				var data = await _service.GetRowsForLookup(keyword, offset, limit, code);
				return ResponseSuccess(data);
			}
			catch (Exception ex)
			{
				return ResponseError(ex);
			}
		}
    }
}
