
namespace Domain.Models
{
    public class SysGeneralCode : BaseModel
    {

        public string? Code {get; set;}
        public string? Description {get; set;}
        public int? IsEditable {get; set;}

    }
}