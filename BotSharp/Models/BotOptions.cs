
namespace QQBot.Net.Models
{
    /// <summary>
    /// 机器人配置
    /// </summary>
    public class BotOptions
    {
        /// <summary>
        /// AppId
        /// </summary>
        public string AppId { get; set; } = string.Empty;

        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret { get; set; } = string.Empty;

        /// <summary>
        /// 访问令牌
        /// </summary>
        public string? Token { get; set; }
    }
}
