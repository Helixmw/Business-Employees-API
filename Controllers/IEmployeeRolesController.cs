using Microsoft.AspNetCore.Mvc;

namespace Employees_API.Controllers
{
    public interface IEmployeeRolesController
    {
        IActionResult AssignRole(int employeeId, int roleId);
        IActionResult ReAssignRole(int employeeId, int oldRoleId, int newRoleId);
        IActionResult RemoveEmployee(int employeeId, int roleId);
    }
}