
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

## API Reference

This section documents the public API of the BotSharp library.

### `IQBotClient`

The main client for interacting with the QQ Bot.

**Properties**

-   `User? CurrentUser { get; }`
    -   **Description**: Gets the current user information after a successful connection.

**Events**

-   `event Action<ReadyPayload> OnReady`
    -   **Description**: Triggered when the bot is ready and has successfully connected.
-   `event Action<Message> OnAtMessageCreated`
    -   **Description**: Triggered when an @ message is received.

**Methods**

-   `Task ProcessWebhookPayloadAsync(string payload)`
    -   **Description**: Processes the raw payload received from the QQ Bot webhook.
    -   **Parameters**:
        -   `payload` (string): The JSON payload from the webhook.

### `IMessageService`

Service for sending messages.

**Methods**

-   `Task<Message> SendMessageAsync(string channelId, string message)`
    -   **Description**: Sends a message to a specified channel.
    -   **Parameters**:
        -   `channelId` (string): The ID of the channel to send the message to.
        -   `message` (string): The content of the message.
    -   **Returns**: A `Task` containing the sent `Message` object.

### `IAuthenticationService`

Service for handling authentication.

**Methods**

-   `Task<string> GetAccessTokenAsync()`
    -   **Description**: Gets the access token required for API calls.
    -   **Returns**: A `Task` containing the access token string.

### `IWebhookValidator`

Service for validating incoming webhooks.

**Methods**

-   `Task<bool> ValidateSignatureAsync(string payload, string signature, string timestamp, string nonce)`
    -   **Description**: Validates the signature of a webhook request to ensure it's from Tencent.
    -   **Parameters**:
        -   `payload` (string): The request body.
        -   `signature` (string): The value of the `X-Tencent-Signature` header.
        -   `timestamp` (string): The value of the `X-Tencent-Timestamp` header.
        -   `nonce` (string): The value of the `X-Tencent-Nonce` header.
    -   **Returns**: A `Task` containing `true` if the signature is valid, otherwise `false`.
