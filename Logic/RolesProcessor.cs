using Employees_API.Data;
using Employees_API.DTOs.Roles;
using Employees_API.Interfaces;
using Employees_API.Models;

namespace Employees_API.Utilities
{
    public class RolesProcessor : BaseContext<Role>, IRolesProcessor
    {
        public RolesProcessor(ApplicationDBContext applicationDBContext) : base(applicationDBContext.DepartmentRoles, applicationDBContext)
        {
            
        }

        public async Task AddAsync(AddRoleDTO Value)
        {
            await this.AddAsync(Value);
        }
        public async Task DeleteAsync(int RoleId)
        {
            await this.GetById(RoleId);
            await this.DeleteAsync(RoleId);
        }
        public async Task EditAsync(EditRoleDTO Value)
        {
            await this.GetById(Value.Id);
            await this.EditAsync(Value);
        }
    }
}
