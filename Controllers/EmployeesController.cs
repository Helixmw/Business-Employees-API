using Employees_API.Data;
using Employees_API.DTOs.Employees;
using Employees_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Employees_API.Models;
using Microsoft.EntityFrameworkCore;
using Employees_API.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace Employees_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase, IController<AddEmployeeDTO, EditEmployeeDTO>, IEmployeesController
    {
        readonly IEmployeesProcessor _employeesProcessor;

        public EmployeesController(IEmployeesProcessor employeesProcessor)
        {
            _employeesProcessor = employeesProcessor;
        }

        //Add New Employee
        [HttpPost]
        public async Task<IActionResult> Post(AddEmployeeDTO Value)
        {

            try
            {
                try
                {
                    await _employeesProcessor.AddEmployeeAsync(Value);
                    return Ok(new { success = true, message = $"Successfully added {Value.Name}" });
                }
                catch (ObjectIsNullException ex)
                {
                    return NotFound(new { success = false, message = ex.Message });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Server Error {ex.Message}" });
            }
        }

       //Get All Employees
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                try
                {
                    var _getEmployees = await _employeesProcessor.GetAllEmployeesAsync();                 
                    return Ok(new { success = true, employees = _getEmployees });
                }
                catch (CollectionIsEmptyException)
                {
                    return NotFound(new { success = false, message = "No employees were found!!" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Server Error {ex.Message}" });
            }
        }

        //Get Employee by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                try
                {
                    var result = await _employeesProcessor.GetEmployeeAsync(id);
                    return Ok(new
                    {
                        success = true,
                        employee = result
                    });
                }
                catch (ObjectIsNullException)
                {
                    return NotFound(new { success = false, message = "Employee was not found." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Server Error {ex.Message}" });
            }
        }

        //Assign Employee to department
        [Route("Assign/{employeeId}/{departmentId}")]
        [HttpGet]
        public async Task<IActionResult> AssignDept(int employeeId, int departmentId)
        {
            try
            {
                try
                {
                    await _employeesProcessor.AssignDepartmentAsync(employeeId, departmentId);
                    return Ok(new { success = true, message = "User department has been updated." });
                }
                catch (ObjectIsNullException ex)
                {
                    return NotFound(new { success = false, message = $"{ex.Message}" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Server Error {ex.Message}" });
            }
        }

        //Delete Employee
        [Route("delete/{id}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                try
                {
                    await _employeesProcessor.DeleteEmployeeAsync(id);
                    return Ok(new { success = true, message = "Employee has been deleted successfully." });
                }
                catch (ObjectIsNullException ex)
                {
                    return NotFound(new { success = false, message = ex.Message });
                }
                } catch (Exception ex)
                {
                    return StatusCode(500, new { success = false, message = $"Server Error {ex.Message}" });
                }
            
          }

        //Update Employee Info
        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> UpdateById(EditEmployeeDTO Value)
        {
            try
            {
                try
                {
                    await _employeesProcessor.UpdateEmployeeAsync(Value);
                    return Ok(new { success = true, message = "Employee has been updated successfully." });
                }
                catch (ObjectIsNullException)
                {
                    return NotFound(new { success = true, message = "This Employee was not found." });
                }
            }catch(Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Server Error {ex.Message}" });
            }
        }

    }



}

