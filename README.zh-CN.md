# BotSharp 解决方案

该代码库包含 BotSharp 库，这是一个用于构建 QQ 机器人的 .NET 解决方案，以及一个用于演示其用法的示例项目。

## 项目

- **BotSharp**: 用于与 QQ 机器人 API 交互的核心类库。有关更多详细信息，请参阅 [BotSharp README](./BotSharp/README.md)。
- **BotSharp.Examples**: 一个示例 ASP.NET Core 项目，演示了如何使用 `BotSharp` 库创建一个功能性的机器人。有关更多详细信息，请参阅 [BotSharp.Examples README](./BotSharp.Examples/README.md)。

## 快速入门

要开始使用示例机器人，请执行以下操作：

1.  **克隆代码库**：
    ```bash
    git clone https://github.com/your-username/BotSharp.git
    cd BotSharp
    ```

2.  **配置示例项目**：
    导航到 `BotSharp.Examples` 目录并打开 `appsettings.json` 文件。填写你的 QQ 机器人凭据：
    ```json
    {
      "QQBot": {
        "AppId": "你的APP_ID",
        "AppSecret": "你的APP_SECRET",
        "Token": "你的TOKEN"
      }
    }
    ```

3.  **运行示例**：
    在你的终端中导航到 `BotSharp.Examples` 目录并运行应用程序：
    ```bash
    cd BotSharp.Examples
    dotnet run
    ```

4.  **设置你的 webhook**：
    机器人将启动并侦听传入的请求。将你的 QQ 机器人平台配置为将 webhook 事件发送到你的应用程序的端点（例如，`https://your-public-url/webhook`）。

## 贡献

欢迎贡献！请随时提出问题或提交拉取请求。
