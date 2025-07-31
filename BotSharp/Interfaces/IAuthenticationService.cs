
namespace QQBot.Net.Interfaces
{
    /// <summary>
    /// 身份验证服务
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// 获取访问令牌
        /// </summary>
        /// <returns>访问令牌</returns>
        Task<string> GetAccessTokenAsync();
    }
}
