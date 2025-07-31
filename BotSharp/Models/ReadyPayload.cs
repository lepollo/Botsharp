
using System.Text.Json.Serialization;

namespace QQBot.Net.Models
{
    /// <summary>
    /// Ready事件的Payload
    /// </summary>
    public class ReadyPayload
    {
        /// <summary>
        /// 版本
        /// </summary>
        [JsonPropertyName("version")]
        public int Version { get; set; }

        /// <summary>
        /// Session ID
        /// </summary>
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; } = string.Empty;

        /// <summary>
        /// 用户
        /// </summary>
        [JsonPropertyName("user")]
        public User User { get; set; } = new();

        /// <summary>
        /// 分片信息
        /// </summary>
        [JsonPropertyName("shard")]
        public int[] Shard { get; set; } = Array.Empty<int>();
    }
}
