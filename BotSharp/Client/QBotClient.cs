
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using QQBot.Net.Enums;
using QQBot.Net.Interfaces;
using QQBot.Net.Models;

namespace QQBot.Net.Client
{
    /// <summary>
    /// QQBot客户端
    /// </summary>
    public class QBotClient : IQBotClient
    {
        private readonly IAuthenticationService _authenticationService;

        /// <inheritdoc />
        public User? CurrentUser { get; private set; }

        /// <inheritdoc />
        public event Action<ReadyPayload>? OnReady;

        /// <inheritdoc />
        public event Action<Message>? OnAtMessageCreated;

        public QBotClient(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <inheritdoc />
        public Task ProcessWebhookPayloadAsync(string payload)
        {
            Console.WriteLine($"[DEBUG] QBotClient: Processing payload. Length: {payload.Length}");
            var gatewayPayload = JsonSerializer.Deserialize<GatewayPayload<JsonElement>>(payload);

            if (gatewayPayload is null)
            {
                Console.WriteLine("[DEBUG] QBotClient: gatewayPayload is null.");
                return Task.CompletedTask;
            }

            Console.WriteLine($"[DEBUG] QBotClient: Gateway OpCode: {gatewayPayload.OpCode}");
            switch ((GatewayOpCode)gatewayPayload.OpCode)
            {
                case GatewayOpCode.Dispatch:
                    Console.WriteLine("[DEBUG] QBotClient: Handling Dispatch.");
                    HandleDispatch(gatewayPayload);
                    break;
                default:
                    Console.WriteLine($"[DEBUG] QBotClient: Unhandled OpCode: {gatewayPayload.OpCode}");
                    break;
            }

            return Task.CompletedTask;
        }

        private void HandleDispatch(GatewayPayload<JsonElement> payload)
        {
            Console.WriteLine($"[DEBUG] QBotClient: Handling EventType: {payload.EventType}");
            switch (payload.EventType)
            {
                case "READY":
                    var readyPayload = payload.Data.Deserialize<ReadyPayload>();
                    if (readyPayload is not null)
                    {
                        CurrentUser = readyPayload.User;
                        OnReady?.Invoke(readyPayload);
                        Console.WriteLine("[DEBUG] QBotClient: OnReady event invoked.");
                    } else {
                        Console.WriteLine("[DEBUG] QBotClient: readyPayload is null.");
                    }
                    break;
                case "AT_MESSAGE_CREATE":
                    var messageJson = payload.Data.ToString();
                    Console.WriteLine($"[DEBUG] QBotClient: Message JSON: {messageJson}");
                    var message = JsonSerializer.Deserialize<Message>(messageJson);
                    if (message is not null)
                    {
                        OnAtMessageCreated?.Invoke(message);
                        Console.WriteLine("[DEBUG] QBotClient: OnAtMessageCreated event invoked.");
                    } else {
                        Console.WriteLine("[DEBUG] QBotClient: message is null.");
                    }
                    break;
                default:
                    Console.WriteLine($"[DEBUG] QBotClient: Unhandled EventType: {payload.EventType}");
                    break;
            }
        }
    }
}
