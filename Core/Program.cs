using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
// using FriendlyBot.Core;

namespace FriendlyBot.Core
{
    class Program
    {
        DiscordSocketClient client;
        CommandHandler handler;

        static void Main(string[] args)
        => new Program().StartAsync().GetAwaiter().GetResult();

        public async Task StartAsync()
        {
            if (Config.Config.botConfig.token == "" || Config.Config.botConfig.token == null)
                return;
            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });

            client.Log += Log;

            await client.LoginAsync(TokenType.Bot, Config.Config.botConfig.token);
            await client.StartAsync();
            handler = new CommandHandler();
            await handler.InitializeAsync(client);
            await Task.Delay(-1);
        }

        private async Task Log(LogMessage msg)
        {
            await Console.Out.WriteLineAsync(msg.Message);
            // Console.WriteLine(msg.Message);
            // return Task.CompletedTask;
        }
    }
}
