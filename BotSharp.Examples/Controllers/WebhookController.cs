
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QQBot.Net.Interfaces;
using QQBot.Net.Models;

namespace QQBot.Net.Examples.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class WebhookController : ControllerBase
    {
        private readonly ILogger<WebhookController> _logger;
        private readonly IQBotClient _botClient;
        private readonly IWebhookValidator _webhookValidator;
        private readonly IMessageService _messageService;

        public WebhookController(ILogger<WebhookController> logger, IQBotClient botClient, IWebhookValidator webhookValidator, IMessageService messageService)
        {
            _logger = logger;
            _botClient = botClient;
            _webhookValidator = webhookValidator;
            _messageService = messageService;

            _botClient.OnAtMessageCreated += OnAtMessageCreated;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            using var reader = new StreamReader(Request.Body);
            var payload = await reader.ReadToEndAsync();

            Request.Headers.TryGetValue("X-Tencent-Signature", out var signature);
            Request.Headers.TryGetValue("X-Tencent-Timestamp", out var timestamp);
            Request.Headers.TryGetValue("X-Tencent-Nonce", out var nonce);

            if (!await _webhookValidator.ValidateSignatureAsync(payload, signature!, timestamp!, nonce!))
            {
                _logger.LogWarning("Signature validation failed.");
                return Unauthorized();
            }

            await _botClient.ProcessWebhookPayloadAsync(payload);

            return Ok();
        }

        private void OnAtMessageCreated(Message message)
        {
            _logger.LogInformation($"Received message from {message.Author.Username}: {message.Content}");
            _messageService.SendMessageAsync(message.ChannelId, "Hello, world!");
        }
    }
}
