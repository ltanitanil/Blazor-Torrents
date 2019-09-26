using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Blazor.Server.BusinessLayer.Entities;
using Blazor.Server.BusinessLayer.Services.AccountService;
using Blazor.Shared.Models.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Server.WebApi.Controllers.Api
{
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountsService;

        public AccountController(IAccountService accountsService, IMapper mapper) : base(mapper)
        {
            _accountsService = accountsService;
        }

        [HttpPost]
        public async Task<string> Login(LoginViewModel loginModel) =>
            await _accountsService.Login(loginModel.Email, loginModel.Password);

        [HttpPost]
        public async Task Register(RegistrationViewModel model) =>
            await _accountsService.Register(_mapper.Map<RegistrationModel>(model));

        [HttpGet]
        public async Task<IEnumerable<string>> LoginProviders() =>
            await _accountsService.GetLoginProviders();

        [HttpGet]
        public IActionResult Login(string providerName)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account");
            var properties = _accountsService.ConfigureExternalAuthenticationProperties(providerName, redirectUrl);
            return Challenge(properties, providerName);
        }

        [HttpGet]
        public async Task<string> ExternalLoginCallback() =>
           await _accountsService.LoginWithExternalIdentifier();
    }
}