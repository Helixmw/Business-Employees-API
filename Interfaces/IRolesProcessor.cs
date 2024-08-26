using Employees_API.DTOs.Roles;

namespace Employees_API.Interfaces
{
    public interface IRolesProcessor
    {
        Task AddAsync(AddRoleDTO roleDTO);

        Task EditAsync(EditRoleDTO roleDTO);

        Task DeleteAsync(int id);
    }
}
