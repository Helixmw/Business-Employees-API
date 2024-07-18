namespace Employees_API.DTOs.Departments
{
    public interface IEditDepartmentDTO
    {
        string? Description { get; set; }
        int Id { get; }
        string? Name { get; set; }
    }
}