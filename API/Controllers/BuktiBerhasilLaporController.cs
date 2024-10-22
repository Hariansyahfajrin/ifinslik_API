using API.GeneralController;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Domain.Abstract.Service;

namespace API.Controllers
{
	[Route("/api/[controller]")]
	[ApiController]
	[SetBaseModelProperties]
	public class BuktiBerhasilLaporController(IBuktiBerhasilLaporService service, IConfiguration configuration) : BaseController(configuration)
	{
		private readonly IBuktiBerhasilLaporService _service = service;

		[HttpGet("GetRows")]
		public async Task<ActionResult> GetRows(string keyword, int offset, int limit,string status)
		{
			try
			{
				var data = await _service.GetRows(keyword, offset, limit,status);
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
		public async Task<ActionResult> Insert(BuktiBerhasilLapor id)
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
		public async Task<ActionResult> UpdateByID(BuktiBerhasilLapor category)
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
        public async Task<ActionResult> ChangeStatus(BuktiBerhasilLapor BuktiBerhasilLapor )
        {
            try
            {
				return ResponseSuccess(null, await _service.ChangeStatus(BuktiBerhasilLapor));
            }
            catch (Exception ex)
            {
                return ResponseError(ex);
            }
        }
		[HttpGet("GetRowsForLookup")]
		public async Task<ActionResult> GetRowsForLookup(string? keyword, int offset, int limit)
		{
			try
			{
				var data = await _service.GetRowsForLookup(keyword, offset, limit);
				return ResponseSuccess(data);
			}
			catch (Exception ex)
			{
				return ResponseError(ex);
			}
		}
		
    
    }
}
