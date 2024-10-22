
namespace Domain.Models
{
    public class BuktiBerhasilLapor : BaseModel
    {
        public string? Code { get; set; }
        public string? CompanyCode { get; set; }
        public string? FinanceCompanyType { get; set; }
        public DateTime? TanggalPelaporan { get; set; }
        public DateTime? TanggalUpload { get; set; }
        public string? PeriodePelaporan { get; set; }
        public string? Month { get; set; }
        public string? Year { get; set; }
        public string? FileName { get; set; }
        public string? Paths { get; set; }
        public string? NamaPelapor { get; set; }
        public string? Notes { get; set; }
        public string? Status { get; set; }
        public int? IsBackup { get; set; }
        public int? IsActive { get; set; }
        public int? IsAutoGenerate { get; set; }

        #region SysGeneralSubCode    
        public string? SysGeneralSubCodeFinanceCompanyType { get; set; }
        #endregion

    }
}