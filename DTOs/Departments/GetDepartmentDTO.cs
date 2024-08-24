namespace Employees_API.DTOs.Departments;

public class GetDepartmentDTO
{
    public int Id
    {
        get; set;
    }
    public string Name { get; set; } = null!;
    public string? Description
    {
        get; set;
    }
}
