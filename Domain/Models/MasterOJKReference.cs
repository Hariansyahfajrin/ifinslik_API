namespace Domain.Models
{
    public class MasterOJKReference : BaseModel
    {
        public string? Code { get; set; }
        public string? ReferenceTypeID { get; set; }
        public string? Description { get; set; }
        public string? OJKCode { get; set; }
        public int? IsActive { get; set; }


        #region SysGeneralSubCode
        public string? SysGeneralSubCodeDescription { get; set; }
        public int? SysGeneralCodeSubIsActive { get; set; }
        #endregion

    }
}