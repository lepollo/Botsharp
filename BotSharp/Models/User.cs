using System.Text.Json.Serialization;

namespace QQBot.Net.Models
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// 用户名
        /// </summary>
        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// 头像
        /// </summary>
        [JsonPropertyName("avatar")]
        public string Avatar { get; set; } = string.Empty;

        /// <summary>
        /// 是否是机器人
        /// </summary>
        [JsonPropertyName("bot")]
        public bool Bot { get; set; }

        /// <summary>
        /// 成员 openid
        /// </summary>
        [JsonPropertyName("member_openid")]
        public string? MemberOpenId { get; set; }

        /// <summary>
        /// 联合用户 ID
        /// </summary>
        [JsonPropertyName("union_openid")]
        public string? UnionOpenId { get; set; }
    }
}