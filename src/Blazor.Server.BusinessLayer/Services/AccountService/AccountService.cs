using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazor.Server.BusinessLayer.Entities;
using Blazor.Server.BusinessLayer.Exceptions;
using Blazor.Server.BusinessLayer.Services.JwtTokenService;
using Blazor.Server.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace Blazor.Server.BusinessLayer.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenService _tokenService;

        public AccountService(SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager,
            IJwtTokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<string> Login(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

            if (!result.Succeeded)
                throw new AppException(ExceptionEvent.LoginFailed, "Username or password is invalid.");

            var user = await _signInManager.UserManager.FindByEmailAsync(email);
            var roles = await _signInManager.UserManager.GetRolesAsync(user);

            return _tokenService.BuildToken(user, roles);
        }

        public async Task Register(RegistrationModel registerModel)
        {
            var newUser = new ApplicationUser
            {
                UserName = registerModel.Email,
                Email = registerModel.Email,
                DateOfBirth = registerModel.DateOfBirth,
                Gender = registerModel.Gender,
                AboutUser = registerModel.AboutUser
            };

            var createResult = await _userManager.CreateAsync(newUser, registerModel.Password);
            if (!createResult.Succeeded)
                throw new AppException(ExceptionEvent.RegistrationFailed, string.Join("\n", createResult.Errors.Select(x => x.Code)));

            await _userManager.AddToRoleAsync(newUser, "User");
        }
    }
}
