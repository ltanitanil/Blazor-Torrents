using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Server.BusinessLayer.Entities;
using Microsoft.AspNetCore.Authentication;

namespace Blazor.Server.BusinessLayer.Services.AccountService
{
    public interface IAccountService
    {
        Task<IEnumerable<string>> GetLoginProviders();
        Task Register(RegistrationModel registrationModel);
        Task<string> Login(string email, string password);
        AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl);
        Task<string> LoginWithExternalIdentifier();
    }
}
