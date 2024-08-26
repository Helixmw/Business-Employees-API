using Employees_API.DTOs.Roles;
using Microsoft.AspNetCore.Mvc;

namespace Employees_API.Controllers;
public interface IRolesController
{
    Task<IActionResult> Add(AddRoleDTO roleDTO);
    Task<IActionResult> Delete(int id);
    Task<IActionResult> Edit(EditRoleDTO roleDTO);
}