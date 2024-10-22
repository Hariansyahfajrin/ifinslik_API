using API.GeneralController;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("/api/[controller]")]
	[ApiController]
	[SetBaseModelProperties]
	public class ValuesController : BaseController
	{
		public readonly IConfiguration _configuration;

		public ValuesController(IConfiguration configuration) : base(configuration)
		{
			_configuration = configuration;
		}

		[HttpGet("Values")]
		public ActionResult GetRows()
		{
			return Ok(new { ID = Guid.NewGuid().ToString().Replace("-", "").ToLower(), Value = "value" });
		}

	}

}