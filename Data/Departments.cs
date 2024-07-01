using Employees_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees_API.Data
{
    public class Departments : BaseContext<Department>
    {
        private readonly ApplicationDBContext _dbContext;
        public Departments(ApplicationDBContext applicationDBContext) : base(applicationDBContext.Departments)
        {
            _dbContext = applicationDBContext;
        }

        public async void RemoveDeptEmployees(int departmentId)
        {
            var employees = await _dbContext.Employees.Where(x => x.DepartmentId == departmentId).ToListAsync();
            _dbContext.Employees.RemoveRange(employees);
        }
    }
}
