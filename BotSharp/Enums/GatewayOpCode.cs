
namespace QQBot.Net.Enums
{
    /// <summary>
    /// 网关操作码
    /// </summary>
    public enum GatewayOpCode
    {
        /// <summary>
        /// 服务端进行消息推送
        /// </summary>
        Dispatch = 0,

        /// <summary>
        /// 客户端或服务端发送心跳
        /// </summary>
        Heartbeat = 1,

        /// <summary>
        /// 客户端发送鉴权
        /// </summary>
        Identify = 2,

        /// <summary>
        /// 客户端恢复连接
        /// </summary>
        Resume = 6,

        /// <summary>
        /// 服务端通知客户端重新连接
        /// </summary>
        Reconnect = 7,

        /// <summary>
        /// 当 identify 或 resume 的时候，如果参数无效的话，服务端会返回该消息
        /// </summary>
        InvalidSession = 9,

        /// <summary>
        /// 当客户端与网关建立 ws 连接之后，网关下发的第一条消息
        /// </summary>
        Hello = 10,

        /// <summary>
        /// 当客户端发送心跳之后，服务端同样会返回一个心跳 ack 消息
        /// </summary>
        HeartbeatAck = 11
    }
}
