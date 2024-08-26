using Employees_API.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employees_API.Models
{
    public class Department : IIdentification, IModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        ICollection<Role>? Roles { get; set; }
        ICollection<Employee>? Employees { get; set; }
    }
}
