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
    public class EmployeesController : ControllerBase, IController<AddEmployeeDTO, EditEmployeeDTO>
    {
        public Employees Employees { get; set; } = null!;
        public Departments Departments { get; set; } = null!;



        public EmployeesController(ApplicationDBContext applicationDBContext)
        {
            Employees = new Employees(applicationDBContext); 
            Departments = new Departments(applicationDBContext);
          

        }

        [HttpGet]

        public async Task<IActionResult> Get()
        {
            try
            {
                try
                {
                    var results = await Employees.GetAllAsync();
                    List<GetEmployeeDTO> employees = new();

                    foreach (var employee in results)
                    {
                        var employeeDTO = new GetEmployeeDTO()
                        {
                            Id = employee.Id,
                            Name = employee.Name,
                            Email = employee.Email,
                            Address = employee.Address,
                            IsAvailable = employee.IsAvailable,
                            DepartmentId = employee.DepartmentId,
                        };
                        employees.Add(employeeDTO);
                    }
                    return Ok(new { success = true, employees = employees });
                                      
                }
                catch (CollectionIsEmptyException)
                {
                    return NotFound(new { success = false, message = "No employees were found" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {success = false,  message = $"Server Error {ex.Message}" });
            }
        }

        [HttpPost]  
        public async Task<IActionResult> Post(AddEmployeeDTO Value)
        {



                try
                {     
                var dept = await Departments.GetById(Value.DepartmentId);
                    if (dept is null)
                        throw new ObjectIsNullException("The chosen department does not exist");


                Employee employee = new Employee()
                {
                    Name = Value.Name,
                    Email = Value.Email,
                    Address = Value.Address,
                    DepartmentId = Value.DepartmentId
                };

                    await Employees.AddAsync(employee);
                    return Ok(new { success = true, message = $"Successfully added {Value.Name}" });

                 }
                    catch(ObjectIsNullException ex)
                  {
                     return NotFound(new { success = false, message = ex.Message });
                  }
            }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                try
                {
                    var result = await Employees.GetById(id);
                    return Ok(new
                    {
                        success = true,
                        employee = new GetEmployeeDTO()
                        {
                            Id = result.Id,
                            Name = result.Name,
                            Email = result.Email,
                            Address = result.Address,
                            DepartmentId = result.DepartmentId

                        }
                    });
                }
                catch (ObjectIsNullException ex)
                {
                    return NotFound(new { success = false, message = $"Employee was not found. {ex.Message}" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Server Error {ex.Message}" });
            }
        }

        [Route("Department/Assign")]
        [HttpPut]
        public async Task<IActionResult> AssignDept(int employeeId, int departmentId)
        {
            try
            {
                try
                {
                    Employees.AssignDepartment(employeeId, departmentId);
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




        [HttpDelete("{id}")]
        public Task<IActionResult> DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public Task<IActionResult> UpdateById(EditEmployeeDTO Value)
        {
            throw new NotImplementedException();
        }

    }


       
    }

