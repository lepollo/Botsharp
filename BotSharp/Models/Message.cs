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
        [JsonPropertyName("id")]
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
        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// 发送时间
        /// </summary>
        [JsonPropertyName("timestamp")]
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
        [JsonPropertyName("author")]
        public User Author { get; set; } = new();

        // 以下是根据文档补充的字段，如果不需要可以删除
        [JsonPropertyName("attachments")]
        public object[]? Attachments { get; set; }

        [JsonPropertyName("embeds")]
        public object[]? Embeds { get; set; }

        [JsonPropertyName("mentions")]
        public User[]? Mentions { get; set; }

        [JsonPropertyName("ark")]
        public object? Ark { get; set; }

        [JsonPropertyName("direct_message")]
        public bool? DirectMessage { get; set; }

        [JsonPropertyName("tts")]
        public bool? Tts { get; set; }

        [JsonPropertyName("pinned")]
        public bool? Pinned { get; set; }

        [JsonPropertyName("type")]
        public int? Type { get; set; }

        [JsonPropertyName("flags")]
        public int? Flags { get; set; }

        [JsonPropertyName("reactions")]
        public object[]? Reactions { get; set; }

        [JsonPropertyName("keyboard")]
        public object? Keyboard { get; set; }

        [JsonPropertyName("sticker")]
        public object? Sticker { get; set; }

        [JsonPropertyName("seq")]
        public int? Seq { get; set; }

        [JsonPropertyName("seq_in_channel")]
        public int? SeqInChannel { get; set; }

        [JsonPropertyName("message_reference")]
        public object? MessageReference { get; set; }

        [JsonPropertyName("group_openid")]
        public string? GroupOpenId { get; set; }

        [JsonPropertyName("src_guild_id")]
        public string? SrcGuildId { get; set; }
    }
}