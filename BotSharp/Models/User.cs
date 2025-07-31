
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
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; } = string.Empty;

        /// <summary>
        /// 是否是机器人
        /// </summary>
        public bool Bot { get; set; }
    }
}
