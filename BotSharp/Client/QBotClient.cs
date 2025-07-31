
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
            var gatewayPayload = JsonSerializer.Deserialize<GatewayPayload<JsonElement>>(payload);

            if (gatewayPayload is null)
            {
                return Task.CompletedTask;
            }

            switch ((GatewayOpCode)gatewayPayload.OpCode)
            {
                case GatewayOpCode.Dispatch:
                    HandleDispatch(gatewayPayload);
                    break;
            }

            return Task.CompletedTask;
        }

        private void HandleDispatch(GatewayPayload<JsonElement> payload)
        {
            switch (payload.EventType)
            {
                case "READY":
                    var readyPayload = payload.Data.Deserialize<ReadyPayload>();
                    if (readyPayload is not null)
                    {
                        CurrentUser = readyPayload.User;
                        OnReady?.Invoke(readyPayload);
                    }
                    break;
                case "AT_MESSAGE_CREATE":
                    var message = payload.Data.Deserialize<Message>();
                    if (message is not null)
                    {
                        OnAtMessageCreated?.Invoke(message);
                    }
                    break;
            }
        }
    }
}
