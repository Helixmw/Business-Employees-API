using Employees_API.Exceptions;
using Employees_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees_API.Data
{
    public class EmployeeRoles
    {
        private readonly ApplicationDBContext applicationDBContext;

        public EmployeeRoles(ApplicationDBContext applicationDBContext)
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
            if(result is not null)
            {
            applicationDBContext.EmployeesRoles.Remove(result);
            await applicationDBContext.SaveChangesAsync();
            }

            else
            throw new ObjectIsNullException("This employee was not assigned a role");
        }

        public async void CheckNewRole(int newRoleId)
        {
            var role = await applicationDBContext.Roles.Where(x => x.Id == newRoleId).FirstOrDefaultAsync();
            if (role is null)
                throw new ObjectIsNullException("This role was not found");
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
            var employee = await applicationDBContext.Employees.Where(x => x.Id == employeeId).FirstOrDefaultAsync();
            if (employee is null)
                throw new ObjectIsNullException("This employee was not found");

            var role = await applicationDBContext.Roles.Where(x => x.Id == roleId).FirstOrDefaultAsync();
            if (role is null)
                throw new ObjectIsNullException("This role was not found");
        }
      
    }
}
