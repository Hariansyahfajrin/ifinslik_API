
namespace Domain.Models
{
    public class TxtStatusOJK : BaseModel
    {
        public string? Code { get; set; }
        public string? CompanyCode { get; set; }
        public string? FinanceCompanyType { get; set; }
        public DateTime? Date { get; set; }
        public string? PeriodeDate { get; set; }


        #region SysGeneralSubCode
        public string? SysGeneralSubCodeFinanceCompanyType { get; set; }
        #endregion


    }
}