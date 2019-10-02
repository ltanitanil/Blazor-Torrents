using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Server.BusinessLayer.Entities;
using Blazor.Server.BusinessLayer.Services.JwtTokenService;
using Blazor.Server.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Blazor.Server.BusinessLayer.Extensions;
using Blazor.Shared.Core.Exceptions;

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

        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl) =>
            _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

        public async Task<IEnumerable<string>> GetLoginProviders() =>
            (await _signInManager.GetExternalAuthenticationSchemesAsync()).Select(x => x.Name) 
            ?? throw new AppException(ExceptionEvent.NotFound);

        public async Task<string> LoginWithExternalIdentifier()
        {
            var loginInfo = await _signInManager.GetExternalLoginInfoAsync();

            if (loginInfo == null)
                throw new AppException(ExceptionEvent.LoginFailed);

            var result = await _signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider, loginInfo.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (!result.Succeeded)
                await RegisterWithLogin(loginInfo);

            var user = await _signInManager.UserManager.FindByLoginAsync(loginInfo.LoginProvider, loginInfo.ProviderKey);
            var roles = await _signInManager.UserManager.GetRolesAsync(user);

            return _tokenService.BuildToken(user, roles);
        }

        public async Task RegisterWithLogin(ExternalLoginInfo externalLoginInfo)
        {
            var email = externalLoginInfo.Principal.Claims
                .FirstOrDefault(x=>x.Type== "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                (await _userManager.CreateAsync(user)).ErrorChecking();
                (await _userManager.AddToRoleAsync(user, "User")).ErrorChecking();
            }
            (await _userManager.AddLoginAsync(user, externalLoginInfo)).ErrorChecking();
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

            (await _userManager.CreateAsync(newUser, registerModel.Password)).ErrorChecking();
            (await _userManager.AddToRoleAsync(newUser, "User")).ErrorChecking();
        }
    }
}
