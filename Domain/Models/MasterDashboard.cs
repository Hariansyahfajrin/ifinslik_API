
namespace Domain.Models
{
    public class MasterDashboard : BaseModel
    {
        public string? Code {get; set;}
        public string? Name {get; set;}
        public string? Type {get; set;}    
        public string? Grip {get; set;}
        public string? SpName {get; set;}
        public string? IsActive {get; set;}
        public string? IsEditable {get; set;}
    }
}