using Employees_API.DTOs.Employees;
using Microsoft.AspNetCore.Mvc;

namespace Employees_API.Controllers
{
    public interface IEmployeesController
    {
        Task<IActionResult> AssignDept(int employeeId, int departmentId);
        Task<IActionResult> DeleteById(int id);
        Task<IActionResult> Get();
        Task<IActionResult> GetById(int id);
        Task<IActionResult> Post(AddEmployeeDTO Value);
        Task<IActionResult> UpdateById(EditEmployeeDTO Value);
    }
}