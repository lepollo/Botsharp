
using Microsoft.Extensions.DependencyInjection;
using QQBot.Net.Client;
using QQBot.Net.Interfaces;

namespace QQBot.Net.Extensions
{
    /// <summary>
    /// ServiceCollection扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加QQBot
        /// </summary>
        /// <param name="services">服务</param>
        /// <returns>服务</returns>
        public static IServiceCollection AddQQBot(this IServiceCollection services)
        {
            services.AddHttpClient("QQBot", client =>
            {
                client.BaseAddress = new("https://api.sgroup.qq.com");
            });

            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IMessageService, MessageService>();
            services.AddSingleton<IQBotClient, QBotClient>();
            services.AddSingleton<IWebhookValidator, WebhookValidator>();

            return services;
        }
    }
}
