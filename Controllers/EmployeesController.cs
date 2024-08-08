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
    public class EmployeesController : ControllerBase, IController<IAddEmployeeDTO, IEditEmployeeDTO>, IEmployeesController
    {
        IEmployee _employeeModel;
        IEmployees _employees;
        IDepartments _departments;
        IGetEmployeeDTO _getEmployeeDTO;
        List<IGetEmployeeDTO> _getEmployees;

        public EmployeesController(
            IEmployees employees,
            IDepartments departments,
            IEmployee employeeModel,
            IGetEmployeeDTO getEmployeeDTO,
            List<IGetEmployeeDTO> getEmployees)
        {
            _getEmployees = getEmployees;
            _employees = employees;
            _departments = departments;
            _employeeModel = employeeModel;
            _getEmployeeDTO = getEmployeeDTO;

        }

        [HttpGet]

        public async Task<IActionResult> Get()
        {
            try
            {
                try
                {
                    var results = await _employees.GetAllAsync();


                    foreach (var employee in results)
                    {

                        _getEmployeeDTO = new GetEmployeeDTO()
                        {
                            Id = employee.Id,
                            Name = employee.Name,
                            Email = employee.Email,
                            Address = employee.Address,
                            IsAvailable = employee.IsAvailable,
                            DepartmentId = employee.DepartmentId,
                        };
                        _getEmployees.Add(_getEmployeeDTO);
                    }
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

        [HttpPost]
        public async Task<IActionResult> Post(IAddEmployeeDTO Value)
        {

            try
            {
                try
                {
                    var dept = await _departments.GetById(Value.DepartmentId);
                    if (dept is null)
                        throw new ObjectIsNullException("The chosen department does not exist");

                    _employeeModel = new Employee()
                    {
                        Name = Value.Name,
                        Email = Value.Email,
                        Address = Value.Address,
                        DepartmentId = Value.DepartmentId
                    };


                    await _employees.AddAsync(_employeeModel);
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                try
                {
                    var result = await _employees.GetById(id);
                    return Ok(new
                    {
                        success = true,
                        _getEmployeeDTO = new GetEmployeeDTO()
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
        public IActionResult AssignDept(int employeeId, int departmentId)
        {
            try
            {
                try
                {
                    _employees.AssignDepartment(employeeId, departmentId);
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
        public Task<IActionResult> UpdateById(IEditEmployeeDTO Value)
        {
            throw new NotImplementedException();
        }

    }



}

