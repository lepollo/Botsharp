
using System.Text.Json.Serialization;

namespace QQBot.Net.Models
{
    /// <summary>
    /// 网关Payload
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class GatewayPayload<T>
    {
        /// <summary>
        /// 操作码
        /// </summary>
        [JsonPropertyName("op")]
        public int OpCode { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        [JsonPropertyName("d")]
        public T? Data { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        [JsonPropertyName("s")]
        public int? Sequence { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>
        [JsonPropertyName("t")]
        public string? EventType { get; set; }
    }
}
