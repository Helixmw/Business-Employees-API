using Employees_API.Data;
using Employees_API.Exceptions;
using Employees_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employees_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeRolesController : ControllerBase
    {

        IEmployeeRoles _employeeRoles;
        public EmployeeRolesController(IEmployeeRoles employeeRoles)
        {
            _employeeRoles = employeeRoles;
        }
       
        [HttpPost]
        public IActionResult AssignRole(int employeeId, int roleId)
        {
            try
            {
                _employeeRoles.AssignRole(employeeId, roleId);
                return Ok(new {success = true, message = "Employee role has been assigned"});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Server Error {ex.Message}" });
            }
        }

     
        [Route("{employeeId}/{oldRoleId}/{newRoleId}")]
        [HttpPut]
        public IActionResult ReAssignRole(int employeeId, int oldRoleId, int newRoleId)
        {
            try
            {
                _employeeRoles.ReAssignRole(employeeId, oldRoleId, newRoleId);
                return Ok(new { success = true, message = "Employee has been assigned a new role" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Server Error {ex.Message}" });
            }
        }

      
        [Route("{employeeId}/{roleId}")]
        [HttpDelete]
        public IActionResult RemoveEmployee(int employeeId, int roleId)
        {
            try
            {
                try
                {
                _employeeRoles.RemoveEmployee(employeeId, roleId);
                return Ok(new { success = true, message = "Employee role has been removed for the role" });
                }catch(ObjectIsNullException ex)
                {
                    return Ok(new { success = false, message = $"{ex.Message}" });

                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Server Error {ex.Message}" });
            }
        }


       

        
        
    }
}
