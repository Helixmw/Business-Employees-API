using Employees_API.Data;
using Employees_API.DTOs.Employees;
using Employees_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Employees_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase, IController<AddEmployeeDTO, EditEmployeeDTO>
    {
         public Employees Employees { get; set; }
        private readonly ApplicationDBContext dbContext;

        public EmployeesController(ApplicationDBContext applicationDBContext)
        {
            Employees = new Employees(applicationDBContext); 
            dbContext = applicationDBContext;
        }

        public Task<IActionResult> Get()
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Post(AddEmployeeDTO Value)
        {
            try
            {
                await Employees.AddAsync(new Employee() { Name = Value.Name, Email = Value.Email, Address = Value.Address, DepartmentId = Value.DepartmentId });
                await dbContext.SaveChangesAsync();
                return Ok(new { success = true, message = $"Successfully added {Value.Name}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Server Error : {ex.Message}" });
            }
        }

        public Task<IActionResult> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> UpdateById(EditEmployeeDTO Value)
        {
            throw new NotImplementedException();
        }
    }
}
