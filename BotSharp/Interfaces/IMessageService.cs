
using System.Threading.Tasks;
using QQBot.Net.Models;

namespace QQBot.Net.Interfaces
{
    /// <summary>
    /// 消息服务
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="channelId">频道ID</param>
        /// <param name="message">消息</param>
        /// <returns>发送的消息</returns>
        Task<Message> SendMessageAsync(string channelId, string message);
    }
}
