using Employees_API.DTOs.Employees;
using Microsoft.AspNetCore.Mvc;

namespace Employees_API.Controllers
{
    public interface IEmployeesController
    {
        IActionResult AssignDept(int employeeId, int departmentId);
        Task<IActionResult> DeleteById(int id);
        Task<IActionResult> Get();
        Task<IActionResult> GetById(int id);
        Task<IActionResult> Post(IAddEmployeeDTO Value);
        Task<IActionResult> UpdateById(IEditEmployeeDTO Value);
    }
}