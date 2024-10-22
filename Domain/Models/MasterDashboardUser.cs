
namespace Domain.Models
{
    public class MasterDashboardUser : BaseModel
    {
        public string? EmployeeCode {get; set;}
        public string? EmployeeName {get; set;}
        public string? DashBoardCode {get; set;}    
        public string? OrderKey {get; set;}

    }
}