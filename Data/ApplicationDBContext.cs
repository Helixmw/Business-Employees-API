using Employees_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Employees_API.Data
{
    public class ApplicationDBContext : IdentityDbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }

        public DbSet<IEmployee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Role> EmployeeRoles {  get; set; }

        public DbSet<EmployeeRole> EmployeesRoles { get; set; }

        public DbSet<DepartmentRole> DepartmentRoles { get; set; }
    }
}
