using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blazor.Server.BusinessLayer.Entities;
using Blazor.Server.DataAccessLayer.Entities;

namespace Blazor.Server.BusinessLayer.Services.AccountService
{
    public interface IAccountService
    {
        Task Register(RegistrationModel registrationModel);
        Task<string> Login(string email, string password);
    }
}
