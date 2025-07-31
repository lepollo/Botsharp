
using System;
using System.Threading.Tasks;
using QQBot.Net.Models;

namespace QQBot.Net.Interfaces
{
    /// <summary>
    /// QQBot客户端
    /// </summary>
    public interface IQBotClient
    {
        /// <summary>
        /// 当前的用户
        /// </summary>
        User? CurrentUser { get; }

        /// <summary>
        /// 当机器人准备好时触发
        /// </summary>
        event Action<ReadyPayload> OnReady;

        /// <summary>
        /// 当收到@消息时触发
        /// </summary>
        event Action<Message> OnAtMessageCreated;

        /// <summary>
        /// 处理Webhook的Payload
        /// </summary>
        /// <param name="payload">Payload</param>
        /// <returns></returns>
        Task ProcessWebhookPayloadAsync(string payload);
    }
}
