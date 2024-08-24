using Employees_API.Data;
using Employees_API.DTOs.Users;
using Employees_API.Exceptions;
using Employees_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Employees_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : ControllerBase, IUserController
    {
        readonly IUsersProcessor _usersProcessor;

        public UserController(IUsersProcessor usersProcessor)
        {
            _usersProcessor = usersProcessor;
        }

        [Route("SignUp")]
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] AddUserDTO user)
        {
            try
            {
                try
                {
                    try
                    {
                        await _usersProcessor.SignUpAsync((AddUserDTO)user);
                        return Ok(new { success = true, message = "You have successfully signed up!" });
                    }
                    catch (InvalidInputException ex)
                    {
                        return BadRequest(new { success = false, message = ex.Message, errors = ex.Errors });
                    }


                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Server Error {ex.Message}" });
            }
        }
        
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(ILoginUserDTO user)
        {
            try
            {
                try
                {
                    var res = await _usersProcessor.LogInAsync((LoginUserDTO)user);
                    if (res is true)
                        return Ok(new { success = true, message = "You have logged in" });

                    return BadRequest(new { success = false, message = "Incorrect Email and Password Combination." });

                }
                catch (ObjectIsNullException ex)
                {
                    return NotFound(new { success = false, message = ex.Message });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [Authorize]
        [Route("Logout")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            try
            {
                try
                {
                    await _usersProcessor.LogOutAsync();
                    return Ok(new { success = true, message = "You have logged out" });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Server Error {ex.Message}" });
            }
        }



    }
}
