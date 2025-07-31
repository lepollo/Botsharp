
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
                return _accessToken;
            }

            var response = await _httpClient.PostAsJsonAsync("https://bots.qq.com/app/get_app_token", new
            {
                appId = _botOptions.AppId,
                clientSecret = _botOptions.AppSecret
            });

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonDocument.Parse(content).RootElement;
            _accessToken = tokenResponse.GetProperty("access_token").GetString();

            return _accessToken!;
        }
    }
}
