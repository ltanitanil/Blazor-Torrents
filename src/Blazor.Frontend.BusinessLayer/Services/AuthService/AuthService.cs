using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Blazor.Frontend.BusinessLayer.Extensions;
using Blazor.Frontend.BusinessLayer.Provider;
using Blazor.Shared.Models.ViewModels;
using Blazor.Shared.Models.ViewModels.Account;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Blazor.Frontend.BusinessLayer.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient,
            AuthenticationStateProvider authenticationStateProvider,
            ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<IEnumerable<string>> GetLoginProviders()
        => await _httpClient.GetJsonAsync<IEnumerable<string>>("api/account/loginproviders");

        public async Task<ResponseModel> Register(RegistrationViewModel registrationViewModel)
        {
            var (successStatusCode, content) = await _httpClient.MyPostJsonAsync<string>("api/account/register", registrationViewModel);

            return new ResponseModel { Successful = successStatusCode, Error = content };
        }

        public async Task<ResponseModel> Login(LoginViewModel loginModel)
        {
            var (successStatusCode, content) = await _httpClient.MyPostJsonAsync<string>("api/account/login", loginModel);

            if (successStatusCode)
            {
                await _localStorage.SetItemAsync("authToken", content);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", content);
                ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(content);
            }

            return new ResponseModel { Successful = successStatusCode, Error = content };
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
