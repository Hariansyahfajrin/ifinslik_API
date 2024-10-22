
namespace Domain.Models
{
    public class MasterTemplateDetail : BaseModel
    {
        public string? MasterTemplateID { get; set; }
        public string? TemplateCode { get; set; }
        public string? PatchPeriode { get; set; }
        public string? FormatType { get; set; }
        public int? OrderRow { get; set; }
        public string? Description { get; set; }
        public string? FieldName { get; set; }
        public string? RefferenceTypeCode { get; set; }
        public int? IsFix { get; set; }
        public int? FixLength { get; set; }
        public string? FixLengthFiller { get; set; }
        public string? FixLengthFillerPosition { get; set; }
        public int? IsActive { get; set; }

        #region SysGeneralSubCode
        public string? SysGeneralSubCodeFormatType { get; set; }
        public string? SysGeneralSubCodeRefferenceTypeCode { get; set; }
        #endregion

    }
}