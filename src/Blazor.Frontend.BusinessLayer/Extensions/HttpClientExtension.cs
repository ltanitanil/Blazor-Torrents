using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blazor.Frontend.BusinessLayer.Extensions
{
    public static class HttpClientExtension
    {
        public static async Task<(bool, T)> MyPostJsonAsync<T>(this HttpClient httpClient, string path, object model)
            where T : class
        {
            using var response = await httpClient.PostAsync(path,
                new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"));

            var responseContent = await response.Content.ReadAsStringAsync();

            return string.IsNullOrWhiteSpace(responseContent) ? (response.IsSuccessStatusCode, null) : 
                (response.IsSuccessStatusCode, JsonSerializer.Deserialize<T>(responseContent));
        }
    }
}
