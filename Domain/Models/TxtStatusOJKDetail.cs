
namespace Domain.Models
{
    public class TxtStatusOJKDetail : BaseModel
    {
        public string? TxtStatusOJKID { get; set; }
        public string? Description { get; set; }
        public string? FormType { get; set; }
        public string? FinanceCompanyType { get; set; }
        public int? IsValid { get; set; }

        #region SysGeneralSubCode
        public string? SysGeneralSubCodeFormType { get; set; }
        public string? SysGeneralSubCodeSlikGroup { get; set; }
        #endregion

        #region TxtStatusOJK
        public string? TxtStatusOJKCode { get; set; }
        public string? TxtStatusOJKCompanyCode { get; set; }
        public string? TxtStatusOJKDate { get; set; }

        #endregion

    }
}