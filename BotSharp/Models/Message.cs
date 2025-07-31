
using System.Text.Json.Serialization;

namespace QQBot.Net.Models
{
    /// <summary>
    /// 消息
    /// </summary>
    public class Message
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// 频道ID
        /// </summary>
        [JsonPropertyName("channel_id")]
        public string ChannelId { get; set; } = string.Empty;

        /// <summary>
        /// 服务器ID
        /// </summary>
        [JsonPropertyName("guild_id")]
        public string GuildId { get; set; } = string.Empty;

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// 发送时间
        /// </summary>
        public string Timestamp { get; set; } = string.Empty;

        /// <summary>
        /// 编辑时间
        /// </summary>
        [JsonPropertyName("edited_timestamp")]
        public string EditedTimestamp { get; set; } = string.Empty;

        /// <summary>
        /// 是否@所有人
        /// </summary>
        [JsonPropertyName("mention_everyone")]
        public bool MentionEveryone { get; set; }

        /// <summary>
        /// 发送者
        /// </summary>
        public User Author { get; set; } = new();
    }
}
