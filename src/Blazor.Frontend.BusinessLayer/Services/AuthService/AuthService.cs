using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Blazor.Frontend.BusinessLayer.Services.CustomHTTPClient;
using Blazor.Frontend.BusinessLayer.Provider;
using Blazor.Shared.Models.ViewModels;
using Blazor.Shared.Models.ViewModels.Account;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Blazor.Shared.Core.Exceptions;

namespace Blazor.Frontend.BusinessLayer.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly CustomHttpClient _customHttpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(CustomHttpClient customHttpClient,
            AuthenticationStateProvider authenticationStateProvider,
            ILocalStorageService localStorage)
        {
            _customHttpClient = customHttpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<IEnumerable<string>> GetLoginProviders()
            => await _customHttpClient.GetJsonAsync<IEnumerable<string>>("api/account/loginproviders");

        public async Task Register(RegistrationViewModel registrationViewModel)
        {
            await _customHttpClient.PostJsonAsync("api/account/register", registrationViewModel
                ?? throw new AppException(ExceptionEvent.InvalidParameters, "Registration form can't be empty."));
        }

        public async Task Login(LoginViewModel loginModel)
        {
            var token = await _customHttpClient.PostJsonAsync<string>("api/account/login", loginModel
                ?? throw new AppException(ExceptionEvent.InvalidParameters, "Login form can't be empty."));
            await SetAsAuthenticated(token);
        }

        public async Task ExternalLogin()
        {
            var token = await _customHttpClient.GetJsonAsync<string>("/api/account/ExternalLoginCallback");
            await SetAsAuthenticated(token);
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _customHttpClient.SetAuthenticationHeaderValue(null);
        }

        private async Task SetAsAuthenticated(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new AppException(ExceptionEvent.InvalidParameters, "Token can't be null or empty.");

            await _localStorage.SetItemAsync("authToken", token);
            _customHttpClient.SetAuthenticationHeaderValue(new AuthenticationHeaderValue("bearer", token));
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(token);
        }
    }
}
