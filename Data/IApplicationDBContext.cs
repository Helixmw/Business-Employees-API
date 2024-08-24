using Employees_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees_API.Data;
public interface IApplicationDBContext
{
    DbSet<DepartmentRole> DepartmentRoles
    {
        get;
        set;
    }
    DbSet<Department> Departments
    {
        get;
        set;
    }
    DbSet<Role> EmployeeRoles
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