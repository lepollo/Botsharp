
using System.Text.Json.Serialization;

namespace QQBot.Net.Models
{
    /// <summary>
    /// Hello事件的Payload
    /// </summary>
    public class HelloPayload
    {
        /// <summary>
        /// 心跳间隔，单位：毫秒
        /// </summary>
        [JsonPropertyName("heartbeat_interval")]
        public int HeartbeatInterval { get; set; }
    }
}
