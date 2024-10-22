namespace Domain.Models
{
    public class SysGlobalParam : BaseModel
    {
        public string? Code {get; set;}
        public string? Description {get; set;}
        public string? Value {get; set;}
        public int? IsEditable {get; set;}

    }
}