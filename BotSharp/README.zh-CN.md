
# BotSharp

BotSharp 是一个用于开发腾讯官方 QQ 机器人（群和频道）的 .NET 库。它提供了一种简单而灵活的方式来与 QQ 机器人平台进行交互，处理 webhook 事件并提供发送消息的服务。

## 功能

- 处理 QQ 机器人 webhook 事件
- 提供与 QQ 机器人 API 交互的客户端
- 包含用于发送和接收消息的服务
- 可扩展和可配置

## 入门指南

### 安装

你可以通过 NuGet 包管理器安装 BotSharp 库：

```bash
dotnet add package BotSharp
```

### 使用方法

要使用 BotSharp，你需要在 `appsettings.json` 文件中配置 `BotOptions`：

```json
{
  "QQBot": {
    "AppId": "你的APP_ID",
    "AppSecret": "你的APP_SECRET",
    "Token": "你的TOKEN"
  }
}
```

然后，在你的 `Program.cs` 文件中，添加 QQBot 服务：

```csharp
using BotSharp.Extensions;
using BotSharp.Models;

var builder = WebApplication.CreateBuilder(args);

// 将服务添加到容器中。
builder.Services.AddQQBot();
builder.Services.Configure<BotOptions>(builder.Configuration.GetSection("QQBot"));

// ...
```

最后，创建一个 webhook 端点来接收来自 QQ 机器人平台的事件：

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
                _logger.LogWarning("签名验证失败。");
                return Unauthorized();
            }

            await _botClient.ProcessWebhookPayloadAsync(payload);

            return Ok();
        }

        private void OnAtMessageCreated(Message message)
        {
            _logger.LogInformation($"收到来自 {message.Author.Username} 的消息：{message.Content}");
            _messageService.SendMessageAsync(message.ChannelId, "你好，世界！");
        }
    }
}
```

## 贡献

欢迎贡献！请随时提交拉取请求。

## API 参考

本节介绍了 BotSharp 库的公共 API。

### `IQBotClient`

用于与 QQ 机器人交互的主要客户端。

**属性**

-   `User? CurrentUser { get; }`
    -   **说明**: 获取连接成功后当前机器人的用户信息。

**事件**

-   `event Action<ReadyPayload> OnReady`
    -   **说明**: 当机器人准备就绪并成功连接时触发。
-   `event Action<Message> OnAtMessageCreated`
    -   **说明**: 当收到 @ 消息时触发。

**方法**

-   `Task ProcessWebhookPayloadAsync(string payload)`
    -   **说明**: 处理从 QQ 机器人 webhook 接收到的原始 payload。
    -   **参数**:
        -   `payload` (string): 来自 webhook 的 JSON payload。

### `IMessageService`

用于发送消息的服务。

**方法**

-   `Task<Message> SendMessageAsync(string channelId, string message)`
    -   **说明**: 向指定频道发送消息。
    -   **参数**:
        -   `channelId` (string): 要发送消息的频道 ID。
        -   `message` (string): 消息内容。
    -   **返回**: 一个包含已发送 `Message` 对象的 `Task`。

### `IAuthenticationService`

用于处理身份验证的服务。

**方法**

-   `Task<string> GetAccessTokenAsync()`
    -   **说明**: 获取 API 调用所需的访问令牌 (access token)。
    -   **返回**: 一个包含访问令牌字符串的 `Task`。

### `IWebhookValidator`

用于验证传入的 webhook 的服务。

**方法**

-   `Task<bool> ValidateSignatureAsync(string payload, string signature, string timestamp, string nonce)`
    -   **说明**: 验证 webhook 请求的签名，以确保它来自腾讯官方。
    -   **参数**:
        -   `payload` (string): 请求体。
        -   `signature` (string): `X-Tencent-Signature` 请求头的值。
        -   `timestamp` (string): `X-Tencent-Timestamp` 请求头的值。
        -   `nonce` (string): `X-Tencent-Nonce` 请求头的值。
    -   **返回**: 如果签名有效，则为包含 `true` 的 `Task`，否则为 `false`。
