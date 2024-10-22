using Microsoft.AspNetCore.Mvc;
using API.GeneralController;
using Domain.Abstract.Service;
using Domain.Models;

namespace API.Controllers
{
	[Route("/api/[controller]")]
	[ApiController]
	[SetBaseModelProperties]
	public class MasterReportUserController : BaseController
	{
		private readonly IMasterReportUserService _service;
		public MasterReportUserController(IMasterReportUserService service, IConfiguration configuration) : base(configuration)
		{
			_service = service;
		}

		#region GetRows
		
		[HttpGet("GetRows")]
		public async Task<ActionResult> GetRows(string? keyword, int offset, int limit)
		{
			try
			{
				List<MasterReportUser> result = await _service.GetRows(keyword, offset, limit);
				return ResponseSuccess(result);
			
			}
			catch (Exception ex)
			{
				return ResponseError(ex);
			}
		}
		#endregion

		#region GetRowByID
		[HttpGet("GetRowByID")]
		public async Task<ActionResult> GetRowByID(string id)
		{
			try
			{
				MasterReportUser result = await _service.GetRowByID(id);
				return ResponseSuccess(result);
				
			}
			catch (Exception ex)
			{
				return ResponseError(ex);
			}
		}
		#endregion

		#region Insert
		[HttpPost("Insert")]
		public async Task<ActionResult> Insert(MasterReportUser model)
		{
			try
			{
				return ResponseSuccess(new {model.ID}, await _service.Insert(model));
			
			}
			catch (Exception ex)
			{
				return ResponseError(ex);
			}
		}
		
		#endregion

		#region UpdateByID
		[HttpPut("UpdateByID")]
		public async Task<ActionResult> UpdateByID(MasterReportUser model)
		{
			try
			{
				return ResponseSuccess(null, await _service.UpdateByID(model));
			
			}
			catch (Exception ex)
			{
				return ResponseError(ex);
			}
		}
		#endregion

		#region DeleteByID
		[HttpDelete("DeleteByID")]
		public async Task<ActionResult> DeleteByID([FromBody] string[] ids)
		{
			try
			{
				return ResponseSuccess(null, await _service.DeleteByID(ids));
			
			}
			catch (Exception ex)
			{
				return ResponseError(ex);
			}
		}
		
		#endregion
	}
}