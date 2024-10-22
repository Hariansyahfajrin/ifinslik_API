using API.GeneralController;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Domain.Abstract.Service;

namespace API.Controllers
{
	[Route("/api/[controller]")]
	[ApiController]
	[SetBaseModelProperties]
	public class F06HistoryController(IF06HistoryService service, IConfiguration configuration) : BaseController(configuration)
	{
		private readonly IF06HistoryService _service = service;

		[HttpGet("GetRows")]
		public async Task<ActionResult> GetRows(string keyword, int offset, int limit, string formTransactionHistoryID)
		{
			try
			{
				var data = await _service.GetRows(keyword, offset, limit, formTransactionHistoryID);
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
		public async Task<ActionResult> Insert(F06History id)
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
		public async Task<ActionResult> UpdateByID(F06History category)
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
		// [HttpGet("GetRowsForLookup")]
		// public async Task<ActionResult> GetRowsForLookup(string keyword, int offset, int limit, string? formTransactionId)
		// {
		// 	try
		// 	{
		// 		var data = await _service.GetRowsForLookup(keyword, offset, limit, formTransactionId);
		// 		return ResponseSuccess(data);
		// 	}
		// 	catch (Exception ex)
		// 	{
		// 		return ResponseError(ex);
		// 	}
		// }



	}
}
