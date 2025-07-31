
using System.Threading.Tasks;

namespace QQBot.Net.Interfaces
{
    /// <summary>
    /// Webhook验证器
    /// </summary>
    public interface IWebhookValidator
    {
        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="payload">请求体</param>
        /// <param name="signature">签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机字符串</param>
        /// <returns>验证是否通过</returns>
        Task<bool> ValidateSignatureAsync(string payload, string signature, string timestamp, string nonce);
    }
}
