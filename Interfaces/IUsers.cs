using Employees_API.DTOs.Users;
using Employees_API.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Employees_API.Interfaces
{
    public interface IUsers
    {
        Task SignUpAsync(IAddUserDTO user);

        Task<bool> LogInAsync(ILoginUserDTO user);

        Task LogOutAsync();

    }
}
