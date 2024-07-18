using Employees_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Employees_API.DTOs.Employees
{
    public class GetEmployeeDTO : IGetEmployeeDTO
    {

        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public Boolean IsAvailable { get; set; } = true;

        public int DepartmentId { get; set; }
    }
}
