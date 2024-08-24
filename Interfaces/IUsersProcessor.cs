using Employees_API.DTOs.Users;
using Employees_API.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Employees_API.Interfaces
{
    public interface IUsersProcessor
    {
        Task SignUpAsync(AddUserDTO user);

        Task<bool> LogInAsync(LoginUserDTO user);

        Task LogOutAsync();

    }
}
