namespace Domain.Models
{
	public class MasterFaq : BaseModel
	{
		public string? Question { get; set; }
		public string? Answer { get; set; }
		public string? Filename { get; set; }
		public string? Paths { get; set; }
		public int? IsActive { get; set; }
	}
}