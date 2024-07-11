using Employees_API.DTOs.Users;
using Employees_API.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Employees_API.Data
{
    public class Users
    {
        public readonly UserManager<IdentityUser> UserManager;
        public readonly SignInManager<IdentityUser> SignInManager;
        public Users(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public async Task SignUpAsync(AddUserDTO user)
        {
            var newuser = new IdentityUser()
            {
                UserName = user.UserName,
                Email = user.Email,
                
            };
            var result = await UserManager.CreateAsync(newuser, user.Password);
            if (result.Succeeded)
                await SignInManager.SignInAsync(newuser, true);

            else
                throw new InvalidInputException("Invalid Entry", result.Errors);

            
          
            
           
            
        }

        public async Task<bool> LogInAsync(LoginUserDTO user)
        {
            var _user = await UserManager.FindByEmailAsync(user.Email);
            if (_user is null)
                throw new ObjectIsNullException("The user by this Email Address was not found.");
          
            var result = await SignInManager.PasswordSignInAsync(_user, user.Password, true, false);

            return result.Succeeded;
        }

        public async Task LogOutAsync()
        {
            await SignInManager.SignOutAsync();
        }
    }
}

