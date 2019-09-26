using Blazor.Shared.Models.ViewModels;
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
        public static async Task<ResponseResult> MyPostJsonAsync(this HttpClient httpClient, string path, object model)
        {
            using var response = await httpClient.PostAsync(path,
                new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"));

            return new ResponseResult
            {
                IsSuccessful = response.IsSuccessStatusCode,
                ContentResult = await response.Content.ReadAsStringAsync()
            };
        }

        public static async Task<ResponseResult> MyGetAsync(this HttpClient httpClient, string path)
        {
            using var response = await httpClient.GetAsync(path);

            return new ResponseResult
            {
                IsSuccessful = response.IsSuccessStatusCode,
                ContentResult = await response.Content.ReadAsStringAsync()
            };
        }
    }
}
