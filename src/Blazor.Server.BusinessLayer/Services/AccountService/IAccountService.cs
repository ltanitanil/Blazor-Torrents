using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Server.BusinessLayer.Services.AccountService
{
    public interface IAccountService
    {
        Task Register(string email, string password, DateTime dateOfBirth, string gender, string userInformation);
        Task<string> Login(string email, string password);
    }
}
