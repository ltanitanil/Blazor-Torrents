using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blazor.Shared.Models.ViewModels.Account;

namespace Blazor.Frontend.BusinessLayer.Services.AuthService
{
    public interface IAuthService
    {
        Task<ResponseModel> Register(RegistrationViewModel registrationViewModel);
        Task<ResponseModel> Login(LoginViewModel loginModel);
        Task Logout();
    }
}
