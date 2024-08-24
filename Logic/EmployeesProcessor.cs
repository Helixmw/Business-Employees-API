using Employees_API.Data;
using Employees_API.DTOs.Employees;
using Employees_API.Exceptions;
using Employees_API.Interfaces;
using Employees_API.Models;
using Microsoft.Identity.Client;

namespace Employees_API.Utilities
{
    public class EmployeesProcessor : BaseContext<Employee>, IEmployeesProcessor
    {
        readonly ApplicationDBContext _dbContext;
        readonly IDepartmentsProcessor _departments;

        public EmployeesProcessor(ApplicationDBContext applicationDBContext, IDepartmentsProcessor departments) : base(applicationDBContext.Employees, applicationDBContext)
        {
            _dbContext = applicationDBContext;
            this._departments = departments;
        }

        public async Task AddEmployeeAsync(AddEmployeeDTO Value)
        {
            var dept = await _departments.GetById(Value.DepartmentId);
            if (dept is null)
                throw new ObjectIsNullException("The chosen department does not exist");
            
            await _dbContext.Employees.AddAsync(new Employee()
            {
                Name = Value.Name,
                Email = Value.Email,
                Address = Value.Address,
                DepartmentId = Value.DepartmentId
            });
            _dbContext.SaveChanges();
        }

        public async Task<List<GetEmployeeDTO>> GetAllEmployeesAsync()
        {
            var results = await this.GetAllAsync();

            var _getEmployees = new List<GetEmployeeDTO>();
            foreach (var employee in results)
            {

                var _getEmployee = new GetEmployeeDTO()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Address = employee.Address,
                    IsAvailable = employee.IsAvailable,
                    DepartmentId = employee.DepartmentId,
                };
                _getEmployees.Add(_getEmployee);
            }
            return _getEmployees;
        }

        public async Task<GetEmployeeDTO> GetEmployeeAsync(int Id)
        {
            var result = await this.GetById(Id);
            

            return new GetEmployeeDTO() { Id = result.Id, Name = result.Name, Email = result.Email, Address = result.Address, DepartmentId = result.DepartmentId};

        }
        public async Task AssignDepartmentAsync(int employeeId, int departmentId)
        {
            var dept = _dbContext.Departments.Where(x => x.Id == departmentId).FirstOrDefault();
            if (dept is null)
                throw new ObjectIsNullException("This department was not found");

            var result = await this.GetById(employeeId);
            if (result is null)
                throw new ObjectIsNullException("This employee was not found");

            result.DepartmentId = departmentId;
            _dbContext.Update(result);
            await _dbContext.SaveChangesAsync();

        }

        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var result = this.GetById(employeeId);
            if (result is null)
                throw new ObjectIsNullException("This employee was not found");
            _dbContext.Remove(result);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(EditEmployeeDTO Value)
        {
            var result = await this.GetById(Value.Id);
            result.Name = Value.Name;
            result.Email = Value.Email;
            result.Address = Value.Address;
            result.IsAvailable = Value.IsAvailable;

            _dbContext.Employees.Update(result);
            await _dbContext.SaveChangesAsync();
        }
    }

}
