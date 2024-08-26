using Employees_API.Data;
using Employees_API.Interfaces;
using Employees_API.Models;
using Employees_API.DTOs.Roles;


namespace Employees_API.Utilities
{
    public class RolesProcessor : BaseContext<Role>, IRoles
    {
        public RolesProcessor(ApplicationDBContext applicationDBContext) : base(applicationDBContext.EmployeeRoles, applicationDBContext)
        {

        }

        public async Task AddAsync(AddRoleDTO roleDTO)
        {
            await this.AddAsync(roleDTO);
        }
        public async Task EditAsync(EditRoleDTO roleDTO)
        {
            await this.GetById(roleDTO.Id);
            await this.EditAsync(roleDTO);
        }
        
        public async Task DeleteAsync(int id)
        {
            await this.GetById(id);
            await this.DeleteAsync(id);

        }
    }
}
