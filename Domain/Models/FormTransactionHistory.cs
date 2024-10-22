
namespace Domain.Models
{
    public class FormTransactionHistory : BaseModel
    {
        public string? Code { get; set; }
        public string? CompanyCode { get; set; }
        public string? FinanceCompanyType { get; set; }
        public string? FormType { get; set; }
        public DateTime? Date { get; set; }

        #region SysGeneralSubCode
        public string? SysGeneralSubCodeFormType { get; set; }
        public string? SysGeneralSubCodeFinanceCompanyType { get; set; }
        #endregion


    }
}