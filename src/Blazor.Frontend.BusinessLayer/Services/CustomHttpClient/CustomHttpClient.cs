using Blazor.Frontend.BusinessLayer.Exceptions;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using System.Threading.Tasks;


namespace Blazor.Frontend.BusinessLayer.Services.CustomHTTPClient
{
    public class CustomHttpClient
    {
        private readonly HttpClient _httpClient;
        public CustomHttpClient(HttpClient httpClient) =>
            _httpClient = httpClient;

        public void SetAuthenticationHeaderValue(AuthenticationHeaderValue authenticationHeaderValue) =>
            _httpClient.DefaultRequestHeaders.Authorization = authenticationHeaderValue;

        public async Task<T> GetJsonAsync<T>(string requestUri) =>
            await SendJsonAsync<T>(HttpMethod.Get, requestUri);

        public async Task PostJsonAsync(string requestUri, object content) =>
            await SendJsonAsync(HttpMethod.Post, requestUri, content);

        public async Task<T> PostJsonAsync<T>(string requestUri, object content) =>
            await SendJsonAsync<T>(HttpMethod.Post, requestUri, content);

        public async Task SendJsonAsync(HttpMethod method, string requestUri, object content) =>
            await SendJsonAsync<IgnoreResponse>(method, requestUri, content);

        public async Task<T> SendJsonAsync<T>(HttpMethod method, string requestUri, object content = null)
        {
            var httpResponseMessage = (content == null)
                ? (await SendAsync(method, requestUri))
                : (await SendAsync(method, requestUri,
                new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json")));

            if (typeof(T) == typeof(IgnoreResponse))
                return default;

            return JsonConvert.DeserializeObject<T>(httpResponseMessage);
        }

        public async Task<string> SendAsync(HttpMethod method, string requestUri, HttpContent content = null)
        {
            var requestMessage = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(requestUri),
                Content = content
            };

            using var httpResponseMessage = await _httpClient.SendAsync(requestMessage);

            if (!httpResponseMessage.IsSuccessStatusCode)
                throw new ResponseException(await httpResponseMessage.Content.ReadAsStringAsync());

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }

        private class IgnoreResponse
        {
        }

    }
}