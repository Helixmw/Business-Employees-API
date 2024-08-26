using Employees_API.Data;
using Employees_API.Exceptions;
using Employees_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Employees_API.Utilities
{
    public class DepartmentRolesProcessor
    {
        private readonly ApplicationDBContext applicationDBContext;

        public DepartmentRolesProcessor(ApplicationDBContext applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }

        public async void AssignRole(int departmentId, int roleId)
        {
            CheckDepartmentAndRole(departmentId, roleId);
            var department = new DepartmentRole()
            {
                DepartmentId = departmentId,
                RoleId = roleId
            };
            await applicationDBContext.AddAsync(department);
            await applicationDBContext.SaveChangesAsync();
        }

        public async void RemoveRole(int departmentId, int roleId)
        {
            CheckDepartmentAndRole(departmentId, roleId);
            var result = await CheckAssignedRole(departmentId, roleId);
            applicationDBContext.DepartmentRoles.Remove(result);
            await applicationDBContext.SaveChangesAsync();
        }

        public async void ReAssignRole(int departmentId, int oldRoleId, int newRoleId)
        {
            CheckDepartmentAndRole(departmentId, oldRoleId);
            CheckNewRole(newRoleId);
            var result = await CheckAssignedRole(departmentId, oldRoleId);
            result.Id = newRoleId;
            await applicationDBContext.SaveChangesAsync();
        }

        public async void CheckNewRole(int newRoleId)
        {
            var role = await applicationDBContext.EmployeesRoles.Where(x => x.Id == newRoleId).FirstOrDefaultAsync();
            if (role is null)
                throw new ObjectIsNullException("This role does not exist");
        }

        public async Task<Role> CheckAssignedRole(int departmentId, int roleId)
        {
            var deptRole = await applicationDBContext.DepartmentRoles.Where(x => x.DepartmentId == departmentId)
                 .Where(x => x.Id == roleId).FirstOrDefaultAsync();
            if (deptRole is null)
                throw new ObjectIsNullException("This role does not exit under this department");

            return deptRole;
        }

        public async void CheckDepartmentAndRole(int departmentId, int roleId)
        {
            var department = await applicationDBContext.Departments.Where(x => x.Id == departmentId).FirstOrDefaultAsync();
            if (department is null)
                throw new ObjectIsNullException("This department does not exist");

            var role = await applicationDBContext.EmployeesRoles.Where(x => x.Id == roleId).FirstOrDefaultAsync();
            if (role is null)
                throw new ObjectIsNullException("This role was not found");
        }
    }
}
