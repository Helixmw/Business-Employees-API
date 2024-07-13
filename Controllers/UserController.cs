using Employees_API.Data;
using Employees_API.DTOs.Users;
using Employees_API.Exceptions;
using Employees_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Employees_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUsers _users;

        public UserController(IUsers users)
        {
           _users = users;
        }

        [Route("SignUp")]
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] AddUserDTO user)
        {
            try
            {
                try
                {
                    await _users.SignUpAsync(user);
                    return Ok(new { success = true, message = "Successfully Signed Up!!!" });
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

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDTO user)
        {
            try
            {
                try
                {
                    var res = await _users.LogInAsync(user);
                    if (res is true)
                        return Ok(new { success = true, message = "You have logged in" });

                    return BadRequest(new { success = false, message = "Incorrect Email and Password Combination." });
                 
                }catch(ObjectIsNullException ex)
                {
                    return NotFound(new { success = false, message = ex.Message });
                }
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("Logout")]

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            try
            {
              await _users.LogOutAsync();
              return Ok(new { success = true, message = "You have logged out" });
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        

    }
}
