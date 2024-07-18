namespace Employees_API.DTOs.Employees
{
    public interface IAddEmployeeDTO
    {
        string? Address { get; set; }
        int DepartmentId { get; set; }
        string? Email { get; set; }
        bool IsAvailable { get; set; }
        string? Name { get; set; }
    }
}