using Employees_API.Data;
using Employees_API.Exceptions;
using Employees_API.Interfaces;
using Employees_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees_API.Utilities
{
    public class EmployeeRolesProcessor : BaseContext<EmployeeRole>, IEmployeeRolesProcessor
    {
        private readonly ApplicationDBContext applicationDBContext;

        public EmployeeRolesProcessor(ApplicationDBContext applicationDBContext):base(applicationDBContext.EmployeesRoles, applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }

        public async Task AssignRole(int employeeId, int roleId)
        {
            CheckEmployeeAndRole(employeeId, roleId);
            var employeeRole = new EmployeeRole() { EmployeeId = employeeId, RoleId = roleId };
            await this.AddAsync(employeeRole);
        }

        public async void ReAssignRole(int employeeId, int oldRoleId, int newRoleId)
        {
            CheckNewRole(newRoleId);
            var role = await CheckOldRole(employeeId, oldRoleId);
            role.RoleId = newRoleId;
            this.Update(role);
        }


        public async Task RemoveEmployee(int employeeId, int roleId)
        {
            CheckEmployeeAndRole(employeeId, roleId);
            var result = await GetEmployeeWithRole(employeeId, roleId);
            if (result is not null)
            {
                this.Delete(result);
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
            await FindEmployee(employeeId);
            var role = await CheckRoleAssignedToEmployee(oldRoleId, employeeId);
            return role;

        }


        public async void CheckEmployeeAndRole(int employeeId, int roleId)
        {
            await FindEmployee(employeeId);
            var role = await this.GetById(roleId);
            
        }

        public async Task<EmployeeRole> GetEmployeeWithRole(int employeeId, int roleId)
        { 
                var result = await applicationDBContext.EmployeesRoles.Where(x => x.EmployeeId == employeeId)
                     .Where(x => x.RoleId == roleId).FirstOrDefaultAsync();          
                return result;        
        }

        public async Task<EmployeeRole> CheckRoleAssignedToEmployee(int oldRoleId, int employeeId)
        {
            var role = await applicationDBContext.EmployeesRoles.Where(x => x.RoleId == oldRoleId)
                .Where(x => x.EmployeeId == employeeId).FirstOrDefaultAsync();
            if (role is null)
                throw new ObjectIsNullException("This role was never assigned to this employee");
            return role;
        }

        private async Task FindEmployee(int id)
        {
            var employee = await applicationDBContext.Employees.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (employee is null)
                throw new ObjectIsNullException("This employee was not found");
        }


    }
}
