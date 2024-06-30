using System.ComponentModel.DataAnnotations.Schema;

namespace Employees_API.Models
{
    public class EmployeesRoles
    {
        public int Id { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public Employee Employee { get; set; }

        public Role Role { get; set; }
    }
}
