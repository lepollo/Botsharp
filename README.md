# BotSharp Solution

This repository contains the BotSharp library, a .NET solution for building QQ bots, along with an example project to demonstrate its usage.

## Projects

- **BotSharp**: The core class library for interacting with the QQ Bot API. For more details, see the [BotSharp README](./BotSharp/README.md).
- **BotSharp.Examples**: An example ASP.NET Core project that demonstrates how to use the `BotSharp` library to create a functional bot. For more details, see the [BotSharp.Examples README](./BotSharp.Examples/README.md).

## Quick Start

To get started with the example bot:

1.  **Clone the repository**:
    ```bash
    git clone https://github.com/your-username/BotSharp.git
    cd BotSharp
    ```

2.  **Configure the example project**:
    Navigate to the `BotSharp.Examples` directory and open the `appsettings.json` file. Fill in your QQ Bot credentials:
    ```json
    {
      "QQBot": {
        "AppId": "YOUR_APP_ID",
        "AppSecret": "YOUR_APP_SECRET",
        "Token": "YOUR_TOKEN"
      }
    }
    ```

3.  **Run the example**:
    Navigate to the `BotSharp.Examples` directory in your terminal and run the application:
    ```bash
    cd BotSharp.Examples
    dotnet run
    ```

4.  **Set up your webhook**:
    The bot will start and listen for incoming requests. Configure your QQ Bot platform to send webhook events to your application's endpoint (e.g., `https://your-public-url/webhook`).

## Contributing

Contributions are welcome! Please feel free to open an issue or submit a pull request.
