using Employees_API.Data;
using Employees_API.Exceptions;
using Employees_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employees_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeRolesController : ControllerBase
    {
        
        public EmployeeRoles EmployeeRoles { get; set; }
        private readonly ApplicationDBContext dbContext;
        public EmployeeRolesController(ApplicationDBContext applicationDBContext)
        {
            EmployeeRoles = new EmployeeRoles(applicationDBContext);
        }
       
        [HttpPost]
        public IActionResult AssignRole(int employeeId, int roleId)
        {
            try
            {
                EmployeeRoles.AssignRole(employeeId, roleId);
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
                EmployeeRoles.ReAssignRole(employeeId, oldRoleId, newRoleId);
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
                EmployeeRoles.RemoveEmployee(employeeId, roleId);
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
