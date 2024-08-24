using Employees_API.DTOs.Employees;
using Employees_API.Exceptions;
using Employees_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees_API.Interfaces
{
    public interface IEmployeesProcessor : IBaseContext<Employee>
    {
        Task AssignDepartmentAsync(int employeeId, int departmentId);

        Task AddEmployeeAsync(AddEmployeeDTO Value);

        Task<List<GetEmployeeDTO>> GetAllEmployeesAsync();

        Task<GetEmployeeDTO> GetEmployeeAsync(int Id);

        Task DeleteEmployeeAsync(int employeeId);

        Task UpdateEmployeeAsync(EditEmployeeDTO Value);

    }
}
