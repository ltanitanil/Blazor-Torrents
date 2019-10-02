using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Shared.Models.ViewModels;
using Blazor.Shared.Models.ViewModels.Account;

namespace Blazor.Frontend.BusinessLayer.Services.AuthService
{
    public interface IAuthService
    {
        Task<IEnumerable<string>> GetLoginProviders();
        Task Register(RegistrationViewModel registrationViewModel);
        Task Login(LoginViewModel loginModel);
        Task ExternalLogin();
        Task Logout();
    }
}
