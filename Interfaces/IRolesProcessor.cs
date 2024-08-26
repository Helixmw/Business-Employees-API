using Employees_API.DTOs.Roles;

namespace Employees_API.Interfaces
{
    public interface IRolesProcessor
    {
        Task AddAsync(AddRoleDTO Value);

        Task EditAsync(EditRoleDTO Value);

        Task DeleteAsync(int RoleId);   

    }
}
