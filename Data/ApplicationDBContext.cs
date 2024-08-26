using Employees_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Employees_API.Data
{
    public class ApplicationDBContext : IdentityDbContext, IApplicationDBContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees
        {
            get; set;
        }

        public DbSet<Department> Departments
        {
            get; set;
        }

        public DbSet<Role> DepartmentRoles
        {
            get; set;
        }

        public DbSet<EmployeeRole> EmployeesRoles
        {
            get; set;
        }

    }
}
