namespace Employees_API.DTOs.Roles;

public class AddRoleDTO
{
    public string Name
    {
        get;
        set;
    }

    public int DepartmentId { get; set; }
}
