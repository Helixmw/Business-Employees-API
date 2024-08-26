using Employees_API.DTOs.Roles;
using Employees_API.Exceptions;
using Employees_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employees_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase, IRolesController
{

    readonly IRolesProcessor _rolesProcessor;

    public RolesController(IRolesProcessor rolesProcessor)
    {
        _rolesProcessor = rolesProcessor;
    }
    public async Task<IActionResult> Add(AddRoleDTO roleDTO)
    {
        try
        {
            await _rolesProcessor.AddAsync(roleDTO);

            return Ok(new { success = true, message = $"Added new role {roleDTO.Name}" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = $"Server Error {ex.Message}"});
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            try
            {
                await _rolesProcessor.DeleteAsync(id);
                return Ok(new { success = true, message = "Role has been deleted" });
            }
            catch (ObjectIsNullException)
                {
                    return NotFound(new { success = false, message = "Role was not found" });
                }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = $"Server Error {ex.Message}" });
        }
    }
    public async Task<IActionResult> Edit(EditRoleDTO roleDTO)
    {
        try
        {
            try
            {
                await _rolesProcessor.EditAsync(roleDTO);
                return Ok(new { success = true, message = $"Updated role {roleDTO.Name}" });
            }
            catch (ObjectIsNullException)
            {
                return NotFound(new { success = false, message = "Role was not found" });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = $"Server Error {ex.Message}" });
        }
    }
}
