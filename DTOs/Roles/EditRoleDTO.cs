namespace Employees_API.DTOs.Roles;

public class EditRoleDTO
{

    public int Id
    {
        get;
        set;
    }
    public string Name
    {
        get;
        set;
    }

    public int DepartmentId
    {
        get; set;
    }
}
