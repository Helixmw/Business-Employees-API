using Employees_API.Data;
using Employees_API.DTOs.Departments;
using Employees_API.Models;
using Employees_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Employees_API.Exceptions.Departments;
using Employees_API.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;

namespace Employees_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentsController : ControllerBase, IController<AddDepartmentDTO, EditDepartmentDTO>, IDepartmentsController
    {
        readonly IDepartmentsProcessor _departmentsProcessor;
       
        public DepartmentsController(IDepartmentsProcessor departmentsProcessor)
        {
            _departmentsProcessor = departmentsProcessor;
            
        }

        //Create New Department
        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddDepartmentDTO Value)
        {
            try
            {
                try
                {
                    await _departmentsProcessor.AddAsync(new Department() { Name = Value.Name, Description = Value.Description });
                   
                    return Ok(new { success = true, message = $"Successfully added {Value.Name}" });
                }
                catch (AddDepartmentException ex)
                {
                    return BadRequest(new { success = false, message = $"Something went wrong. Try again later {ex.Message}" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Server Error : {ex.Message}" });
            }
        }
       
        //List All Departments
        [Route("List")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                try
                {
                    var results = await _departmentsProcessor.GetAllAsync();
                    return Ok(new { success = true, departments = results });
                }
                catch (CollectionIsEmptyException)
                {
                    return NotFound(new { success = false, message = "No departments were found" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Server Error : {ex.Message}" });
            }
        }

        //Delete Department and Associated Employees
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                try
                {
                    var result = await _departmentsProcessor.GetById(id);
                    try
                    {
                        
                        _departmentsProcessor.RemoveDeptEmployees(result);
                        return Ok(new { success = true, departments = $"Department was deleted." });
                    }
                    catch (ObjectDeleteException)
                    {
                        return BadRequest(new { success = false, departments = $"Unable to deleted {result}." });
                    }
                }
                catch (ObjectIsNullException)
                {
                    return NotFound(new { success = false, message = "This department is not found." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Server Error : {ex.Message}" });
            }
        }
 
        //Get Department by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                try
                {
                    var result = await _departmentsProcessor.GetById(id);
                    return Ok(new { success = true, department = result });
                }
                catch (ObjectIsNullException)
                {
                    return NotFound(new { success = false, message = "This department is not found." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Server Error : {ex.Message}" });
            }
        }

        //Update Department by Id
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EditDepartmentDTO Value)
        {
            try
            {
                try
                {                
                    await _departmentsProcessor.UpdateDepartmentAsync(new Department() { Id = Value.Id, Name = Value.Name, Description = Value.Description });                               
                    return Ok(new { success = true, message = $"Successfully updated {Value.Name}" });
                }
                catch (UpdateDepartmentException ex)
                {
                    return BadRequest(new { success = false, message = $"Something went wrong. Try again later {ex.Message}" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Server Error : {ex.Message}" });
            }
        }

        //Empty Department Employees
        [HttpPut("{departmentId}")]
        public async Task<IActionResult> RemoveDeptEmployees([FromBody] GetDepartmentDTO dept)
        {
            try
            {
                try
                {
                    _departmentsProcessor.RemoveDeptEmployees(new Department { Id = dept.Id, Name = dept.Name, Description = dept.Description});
                    return Ok(new
                    {
                        success = true,
                        message = "Delete all Employees from department"
                    });
                }catch(UpdateDepartmentException ex)
                {
                    return BadRequest(new { success = false, message = $"Something went wrong. Try again later {ex.Message}" });
                }
            }catch(Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Server Error : {ex.Message}" });
            }
        }
    }
}
