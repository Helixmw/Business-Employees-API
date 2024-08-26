using Employees_API.Data;
using Employees_API.Exceptions;
using Employees_API.Interfaces;
using Employees_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees_API.Utilities
{
    public class EmployeeRolesProcessor : BaseContext<DepartmentRole>, IEmployeeRolesProcessor
    {
        private readonly ApplicationDBContext applicationDBContext;

        public EmployeeRolesProcessor(ApplicationDBContext applicationDBContext):base(applicationDBContext.DepartmentRoles, applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }

        public async void AssignRole(int employeeId, int roleId)
        {
            CheckEmployeeAndRole(employeeId, roleId);
            var employeeRole = new EmployeeRole()
            {
                EmployeeId = employeeId,
                RoleId = roleId
            };
            applicationDBContext.EmployeesRoles.Add(employeeRole);
            await applicationDBContext.SaveChangesAsync();

        }

        public async void ReAssignRole(int employeeId, int oldRoleId, int newRoleId)
        {
            CheckNewRole(newRoleId);
            var role = await CheckOldRole(employeeId, oldRoleId);
            role.RoleId = newRoleId;
            applicationDBContext.EmployeesRoles.Update(role);
            await applicationDBContext.SaveChangesAsync();

        }


        public async void RemoveEmployee(int employeeId, int roleId)
        {
            CheckEmployeeAndRole(employeeId, roleId);
            var result = await applicationDBContext.EmployeesRoles.Where(x => x.EmployeeId == employeeId)
                   .Where(x => x.RoleId == roleId).FirstOrDefaultAsync();
            if (result is not null)
            {
                applicationDBContext.EmployeesRoles.Remove(result);
                await applicationDBContext.SaveChangesAsync();
            }

            else
                throw new ObjectIsNullException("This employee was not assigned a role");
        }

        public async void CheckNewRole(int newRoleId)
        {
            var role = await this.GetById(newRoleId);
          
        }


        public async Task<EmployeeRole> CheckOldRole(int employeeId, int oldRoleId)
        {
            var employee = await applicationDBContext.Employees.Where(x => x.Id == employeeId).FirstOrDefaultAsync();
            if (employee is null)
                throw new ObjectIsNullException("This employee was not found");

            var role = await applicationDBContext.EmployeesRoles.Where(x => x.RoleId == oldRoleId)
                 .Where(x => x.EmployeeId == employeeId).FirstOrDefaultAsync();
            if (role is null)
                throw new ObjectIsNullException("This role was never assigned to this employee");

            return role;

        }


        public async void CheckEmployeeAndRole(int employeeId, int roleId)
        {
            var employee = await FindEmployee(employeeId);
            var role = await this.GetById(roleId);
            
        }

        private async Task<Employee> FindEmployee(int id)
        {
            var employee = await applicationDBContext.Employees.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (employee is null)
                throw new ObjectIsNullException("This employee was not found");

            return employee;
        }


    }
}
