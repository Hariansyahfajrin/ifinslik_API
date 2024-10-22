namespace Domain.Models
{
	public class MasterValidation : BaseModel
	{
		public string? Code { get; set; }
		public string? FinanceCompanyType { get; set; }
		public string? FormType { get; set; }
		public string? Description { get; set; }
		public string? FunctionName { get; set; }
		public int? IsActive { get; set; }
	}
}