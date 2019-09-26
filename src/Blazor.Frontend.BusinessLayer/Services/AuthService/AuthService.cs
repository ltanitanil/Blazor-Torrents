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

        public async Task<ResponseResult> Register(RegistrationViewModel registrationViewModel) 
            => await _httpClient.MyPostJsonAsync("api/account/register", registrationViewModel);

        public async Task<ResponseResult> Login(LoginViewModel loginModel)
        {
            var result = await _httpClient.MyPostJsonAsync("api/account/login", loginModel);
            if (result.IsSuccessful)
                await SetAsAuthenticated(result.ContentResult);
            return result;
        }

        public async Task<ResponseResult> ExternalLogin()
        {
            var result = await _httpClient.MyGetAsync("/api/account/ExternalLoginCallback");
            if(result.IsSuccessful)
                await SetAsAuthenticated(result.ContentResult);
            return result;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        private async Task SetAsAuthenticated(string token)
        {
            await _localStorage.SetItemAsync("authToken", token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(token);
        }
    }
}
