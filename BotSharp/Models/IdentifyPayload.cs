
using System.Text.Json.Serialization;

namespace QQBot.Net.Models
{
    /// <summary>
    /// Identify事件的Payload
    /// </summary>
    public class IdentifyPayload
    {
        /// <summary>
        /// 访问令牌
        /// </summary>
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// 连接意图
        /// </summary>
        [JsonPropertyName("intents")]
        public int Intents { get; set; }

        /// <summary>
        /// 分片信息
        /// </summary>
        [JsonPropertyName("shard")]
        public int[]? Shard { get; set; }
    }
}
