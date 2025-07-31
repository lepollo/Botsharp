
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using QQBot.Net.Interfaces;
using QQBot.Net.Models;

namespace QQBot.Net.Client
{
    /// <summary>
    /// Webhook验证器
    /// </summary>
    public class WebhookValidator : IWebhookValidator
    {
        private readonly BotOptions _botOptions;

        public WebhookValidator(IOptions<BotOptions> botOptions)
        {
            _botOptions = botOptions.Value;
        }

        /// <inheritdoc />
        public Task<bool> ValidateSignatureAsync(string payload, string signature, string timestamp, string nonce)
        {
            var token = _botOptions.Token;

            if (string.IsNullOrEmpty(token))
            {
                return Task.FromResult(false);
            }

            var parameters = new[] { token, timestamp, nonce, payload };
            System.Array.Sort(parameters, System.StringComparer.Ordinal);

            var combinedString = string.Concat(parameters);

            using var sha1 = SHA1.Create();
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(combinedString));
            var hashString = new StringBuilder();
            foreach (var b in hash)
            {
                hashString.Append(b.ToString("x2"));
            }

            return Task.FromResult(hashString.ToString() == signature);
        }
    }
}
