namespace Domain.Models
{
    public class SysGeneralSubCode : BaseModel
    {
        public string? SysGeneralCodeID { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public int? OrderKey { get; set; }
        public int? IsActive { get; set; }

        #region SysGeneralCode
        public string? SysGeneralCodeDescription { get; set; }
        public int? SysGeneralCodeIsEditable { get; set; }
        #endregion
        // public SysGeneralCode? SysGeneralCode {get; set;}

    }
}