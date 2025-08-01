
# BotSharp

BotSharp is a .NET library for developing QQ bots. It provides a simple and flexible way to interact with the QQ Bot platform, handling webhook events and providing services for sending messages.

## Features

- Handles QQ Bot webhook events
- Provides a client for interacting with the QQ Bot API
- Includes services for sending and receiving messages
- Extensible and configurable

## Getting Started

### Installation

You can install the BotSharp library via NuGet Package Manager:

```bash
dotnet add package BotSharp
```

### Usage

To use BotSharp, you need to configure the `BotOptions` in your `appsettings.json` file:

```json
{
  "QQBot": {
    "AppId": "YOUR_APP_ID",
    "AppSecret": "YOUR_APP_SECRET",
    "Token": "YOUR_TOKEN"
  }
}
```

Then, in your `Program.cs` file, add the QQBot services:

```csharp
using BotSharp.Extensions;
using BotSharp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddQQBot();
builder.Services.Configure<BotOptions>(builder.Configuration.GetSection("QQBot"));

// ...
```

Finally, create a webhook endpoint to receive events from the QQ Bot platform:

```csharp
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BotSharp.Interfaces;
using BotSharp.Models;

namespace BotSharp.Examples.Controllers
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
```

## Contributing

Contributions are welcome! Please feel free to submit a pull request.
