
using System;

namespace QQBot.Net.Enums
{
    /// <summary>
    /// 连接意图
    /// </summary>
    [Flags]
    public enum Intents
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,

        /// <summary>
        /// 频道
        /// </summary>
        Guilds = 1 << 0,

        /// <summary>
        /// 频道成员
        /// </summary>
        GuildMembers = 1 << 1,

        /// <summary>
        /// 频道消息
        /// </summary>
        GuildMessages = 1 << 9,

        /// <summary>
        /// 频道消息反应
        /// </summary>
        GuildMessageReactions = 1 << 10,

        /// <summary>
        /// 私信
        /// </summary>
        DirectMessage = 1 << 12,

        /// <summary>
        /// 消息审核
        /// </summary>
        MessageAudit = 1 << 27,

        /// <summary>
        /// 论坛
        /// </summary>
        Forum = 1 << 28,

        /// <summary>
        /// 音频
        /// </summary>
        AudioAction = 1 << 29,

        /// <summary>
        /// @消息
        /// </summary>
        AtMessages = 1 << 30
    }
}
