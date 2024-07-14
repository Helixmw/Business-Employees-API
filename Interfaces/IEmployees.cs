using Employees_API.Exceptions;
using Employees_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees_API.Interfaces
{
    public interface IEmployees : IBaseContext<Employee>
    {
        void AssignDepartment(int employeeId, int departmentId);

    }
}
