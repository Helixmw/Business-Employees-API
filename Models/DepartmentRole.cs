using System.ComponentModel.DataAnnotations.Schema;

namespace Employees_API.Models
{
    public class DepartmentRole
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
    }
}
