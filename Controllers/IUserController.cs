using Employees_API.DTOs.Users;
using Microsoft.AspNetCore.Mvc;

namespace Employees_API.Controllers
{
    public interface IUserController
    {
        Task<IActionResult> Login(ILoginUserDTO user);
        Task<IActionResult> Logout();
        Task<IActionResult> SignUp([FromBody] IAddUserDTO user);
    }
}