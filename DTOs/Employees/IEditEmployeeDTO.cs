namespace Employees_API.DTOs.Employees
{
    public interface IEditEmployeeDTO
    {
        string? Address { get; set; }
        int DepartmentId { get; set; }
        string? Email { get; set; }
        int Id { get; }
        bool IsAvailable { get; set; }
        string? Name { get; set; }
    }
}