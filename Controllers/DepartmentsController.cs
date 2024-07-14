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
    public class DepartmentsController : ControllerBase, IController<AddDepartmentDTO, EditDepartmentDTO>
    {
        IDepartments _departments;
        readonly ApplicationDBContext dBContext;
        public DepartmentsController(ApplicationDBContext applicationDBContext, IDepartments departments)
        {
            _departments = departments;
            dBContext = applicationDBContext;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                try
                {
                    var result = await _departments.GetById(id);
                    try
                    {
                        _departments.Delete(result);
                        _departments.RemoveDeptEmployees(id);
                        await dBContext.SaveChangesAsync();
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                try
                {
                    var results = await _departments.GetAllAsync();
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                try
                {
                    var result = await _departments.GetById(id);
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


        [HttpPost]
        public async Task<IActionResult> Post(AddDepartmentDTO Value)
        {
            try
            {
                try
                {
                    await _departments.AddAsync(new Department() { Name = Value.Name, Description = Value.Description });
                    await dBContext.SaveChangesAsync();
                    return Ok(new { success = true, message = $"Successfully added {Value.Name}" });
                }
                catch (AddDepartmentException ex)
                {
                    return BadRequest(new { success = false, message = $"Something went wrong. Try again later {ex.Message}" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {success = false, message = $"Server Error : {ex.Message}"});
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateById([FromBody] EditDepartmentDTO Value)
        {
            try
            {
                try
                {
                    _departments.Update(new Department() { Id = Value.Id, Name = Value.Name, Description = Value.Description });
                    await dBContext.SaveChangesAsync();
                    return Ok(new { success = true, message = $"Successfully updated {Value.Name}" });
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
