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
        Task<ResponseModel> Register(RegistrationViewModel registrationViewModel);
        Task<ResponseModel> Login(LoginViewModel loginModel);
        Task Logout();
    }
}
