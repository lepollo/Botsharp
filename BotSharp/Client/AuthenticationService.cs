
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using QQBot.Net.Interfaces;
using QQBot.Net.Models;

namespace QQBot.Net.Client
{
    /// <summary>
    /// 身份验证服务
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly BotOptions _botOptions;
        private string? _accessToken;

        public AuthenticationService(IHttpClientFactory httpClientFactory, IOptions<BotOptions> botOptions)
        {
            _httpClient = httpClientFactory.CreateClient("QQBot");
            _botOptions = botOptions.Value;
        }

        /// <inheritdoc />
        public async Task<string> GetAccessTokenAsync()
        {
            if (!string.IsNullOrEmpty(_accessToken))
            {
                Console.WriteLine("[DEBUG] AuthenticationService: Using cached access token.");
                return _accessToken;
            }

            Console.WriteLine("[DEBUG] AuthenticationService: Requesting new access token...");
            var requestBody = new
            {
                appId = _botOptions.AppId,
                clientSecret = _botOptions.AppSecret
            };
            Console.WriteLine($"[DEBUG] AuthenticationService: Request URL: https://bots.qq.com/app/get_app_token");
            Console.WriteLine($"[DEBUG] AuthenticationService: Request Body: {JsonSerializer.Serialize(requestBody)}");

            var response = await _httpClient.PostAsJsonAsync("https://bots.qq.com/app/get_app_token", requestBody);

            Console.WriteLine($"[DEBUG] AuthenticationService: Response Status Code: {response.StatusCode}");
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"[DEBUG] AuthenticationService: Response Body: {content}");

            response.EnsureSuccessStatusCode();

            var tokenResponse = JsonDocument.Parse(content).RootElement;
            _accessToken = tokenResponse.GetProperty("access_token").GetString();
            Console.WriteLine("[DEBUG] AuthenticationService: Access token obtained.");

            return _accessToken!;
        }
    }
}
