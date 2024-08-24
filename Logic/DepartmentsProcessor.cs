using Employees_API.Data;
using Employees_API.Interfaces;
using Employees_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees_API.Utilities
{
    public class DepartmentsProcessor : BaseContext<Department>, IDepartmentsProcessor
    {
        private readonly ApplicationDBContext _dbContext;
        public DepartmentsProcessor(ApplicationDBContext applicationDBContext) : base(applicationDBContext.Departments, applicationDBContext)
        {
            _dbContext = applicationDBContext;
        }

        public async void RemoveDeptEmployees(Department dept)
        {

            _dbContext.Departments.Remove(dept);
            var employees = await _dbContext.Employees.Where(x => x.DepartmentId == dept.Id).ToListAsync();
            if (employees.Count > 0)
            {
                _dbContext.Employees.RemoveRange(employees);
            }
                _dbContext.SaveChanges();
        }

        public void SaveDeptChanges()
        {
            _dbContext.SaveChanges();
        }


    }
}
