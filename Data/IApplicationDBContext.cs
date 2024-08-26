using Employees_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees_API.Data;
public interface IApplicationDBContext
{
   
    DbSet<Department> Departments
    {
        get;
        set;
    }
    DbSet<Role> DepartmentRoles
    {
        get;
        set;
    }
    DbSet<Employee> Employees
    {
        get;
        set;
    }
    DbSet<EmployeeRole> EmployeesRoles
    {
        get;
        set;
    }
}