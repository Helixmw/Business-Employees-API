using Employees_API.Exceptions;
using Employees_API.Interfaces;
using Employees_API.Models;
using Microsoft.Identity.Client;

namespace Employees_API.Data
{
    public class Employees : BaseContext<Employee>
    {
        private readonly ApplicationDBContext _dbContext;
        public Employees(ApplicationDBContext applicationDBContext) : base(applicationDBContext.Employees)
        {
            _dbContext = applicationDBContext;
        }

       

        public void AssignDepartment(int employeeId, int departmentId)
        {
            var dept = _dbContext.Departments.Where(x => x.Id == departmentId).FirstOrDefault();
            if (dept is null)
                throw new ObjectIsNullException("This department was not found");

            var result = _dbContext.Employees.Where(x => x.Id == employeeId).FirstOrDefault();
            if (result is null)
                throw new ObjectIsNullException("This employee was not found");

            result.DepartmentId = departmentId;
            _dbContext.Update(result);
   
        }
    }

}
