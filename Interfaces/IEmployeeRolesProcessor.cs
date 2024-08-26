using Employees_API.Data;
using Employees_API.Exceptions;
using Employees_API.Models;

namespace Employees_API.Interfaces
{
    public interface IEmployeeRolesProcessor
    {
        void AssignRole(int employeeId, int roleId);


        void ReAssignRole(int employeeId, int oldRoleId, int newRoleId);



        void RemoveEmployee(int employeeId, int roleId);


        void CheckNewRole(int newRoleId);



        Task<EmployeeRole> CheckOldRole(int employeeId, int oldRoleId);



        void CheckEmployeeAndRole(int employeeId, int roleId);

    }
}
