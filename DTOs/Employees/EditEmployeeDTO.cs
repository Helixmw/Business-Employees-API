using System.ComponentModel.DataAnnotations;

namespace Employees_API.DTOs.Employees
{
    public class EditEmployeeDTO
    {
        public int Id { get; private set; }

        [Required(ErrorMessage = "Please provide Employee Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide an Email Address")]
        [EmailAddress(ErrorMessage = "Please provide a valid Email Address")]
        public string Email { get; set; }

        public string? Address { get; set; }

        public Boolean IsAvailable { get; set; } = true;

        [Required(ErrorMessage = "Please select which Department the Employee will be in")]
        public int DepartmentId { get; set; }
    }
}
