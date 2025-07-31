
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using QQBot.Net.Interfaces;
using QQBot.Net.Models;

namespace QQBot.Net.Client
{
    /// <summary>
    /// 消息服务
    /// </summary>
    public class MessageService : IMessageService
    {
        private readonly HttpClient _httpClient;

        public MessageService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("QQBot");
        }

        /// <inheritdoc />
        public async Task<Message> SendMessageAsync(string channelId, string message)
        {
            var response = await _httpClient.PostAsJsonAsync($"/channels/{channelId}/messages", new
            {
                content = message
            });

            response.EnsureSuccessStatusCode();

            return (await response.Content.ReadFromJsonAsync<Message>())!;
        }
    }
}
