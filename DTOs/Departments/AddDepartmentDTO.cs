using System.ComponentModel.DataAnnotations;

namespace Employees_API.DTOs.Departments
{
    public class AddDepartmentDTO : IAddDepartmentDTO
    {
        [Required(ErrorMessage = "Please provide a department name")]
        public string Name { get; set; }

        public string? Description { get; set; }

    }
}
