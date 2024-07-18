namespace Employees_API.DTOs.Departments
{
    public interface IAddDepartmentDTO
    {
        string? Description { get; set; }
        string Name { get; set; }
    }
}