using Employees_API.Data;
using Employees_API.Interfaces;
using Employees_API.Models;

namespace Employees_API.Utilities
{
    public class RolesProcessor : BaseContext<Role>, IRoles
    {
        public RolesProcessor(ApplicationDBContext applicationDBContext) : base(applicationDBContext.EmployeeRoles, applicationDBContext)
        {
<<<<<<< HEAD

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
=======

>>>>>>> efa45306edcdb9ba95c71256745bb5f585e22c79
        }
    }
}
