using Employees_API.Data;
using Employees_API.Interfaces;
using Employees_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees_API.Utilities
{
    public class Departments : BaseContext<Department>, IDepartments
    {
        private readonly ApplicationDBContext _dbContext;
        public Departments(ApplicationDBContext applicationDBContext) : base(applicationDBContext.Departments, applicationDBContext)
        {
            _dbContext = applicationDBContext;
        }

        public async void RemoveDeptEmployees(int departmentId)
        {
            var employees = await _dbContext.Employees.Where(x => x.DepartmentId == departmentId).ToListAsync();
            _dbContext.Employees.RemoveRange(employees);
            _dbContext.SaveChanges();
        }

        public void SaveDeptChanges()
        {
            _dbContext.SaveChanges();
        }


    }
}
