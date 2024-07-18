namespace Employees_API.DTOs.Employees
{
    public interface IGetEmployeeDTO
    {
        string? Address { get; set; }
        int DepartmentId { get; set; }
        string? Email { get; set; }
        int Id { get; set; }
        bool IsAvailable { get; set; }
        string? Name { get; set; }
    }
}