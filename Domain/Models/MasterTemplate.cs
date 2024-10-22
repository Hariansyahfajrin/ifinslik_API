
namespace Domain.Models
{
    public class MasterTemplate : BaseModel
    {
        public string? Code { get; set; }
        public string? FinanceCompanyType { get; set; }
        public string? FormType { get; set; }
        public string? Description { get; set; }
        public string? DelimiterStart { get; set; }
        public string? DelimiterCenter { get; set; }
        public string? DelimiterEnd { get; set; }
        public int? IsActive { get; set; }

        #region SysGeneralSubCode
        public string? SysGeneralSubCodeFormType { get; set; }
        public string? SysGeneralSubCodeFinanceCompanyType { get; set; }
        #endregion



    }
}