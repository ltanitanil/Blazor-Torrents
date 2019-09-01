using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blazor.Server.BusinessLayer.Services.AccountService;
using Blazor.Shared.Models.ViewModels.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Server.WebApi.Controllers.Api
{
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountsService;

        public AccountController(IAccountService accountsService) 
        {
            _accountsService = accountsService;
        }

        [HttpPost]
        public async Task<string> Login(LoginViewModel loginModel) => 
            await _accountsService.Login(loginModel.Email, loginModel.Password);

        [HttpPost]
        public async Task Register(RegistrationViewModel model) =>
            await _accountsService.Register(model.Email, model.Password, model.DateOfBirth, model.Gender,
                model.AboutUser);
    }
}