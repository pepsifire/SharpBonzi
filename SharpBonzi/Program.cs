using System;
using DSharpPlus;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSPlus.Examples;
using Newtonsoft.Json;
using System.IO;
using DSharpPlus.Net.WebSocket;
using DSharpPlus.Entities;

namespace SharpBonzi
{
    class Program
    {
        public static DiscordClient discord;
        static CommandsNextModule commands;
        static void Main(string[] args)
        {
            Console.Title = "SharpBonzi";
            if (!File.Exists("config.json")) {
                Console.WriteLine("Error! Missing config.json!"); 
                Console.ReadLine();
                System.Environment.Exit(1);
            }
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            var json = "";
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync();

            var cfgjson = JsonConvert.DeserializeObject<ConfigJson>(json);
            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = cfgjson.Token,
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Error
            });
            discord.SetWebSocketClient<WebSocketSharpClient>();
            
            discord.GuildCreated += async e =>
            {
                var defaultChannel = e.Guild.GetDefaultChannel();
                Console.Write($"Joined: {defaultChannel}");
                await discord.SendMessageAsync(defaultChannel, "Bonzi in da HOUSE");
            };
            
            discord.Ready += async e =>
            {
                await Task.Run(() =>
                {
                    Console.WriteLine("Initialized SharpBonzi!");
                    Console.WriteLine($"Connected to {discord.Guilds.Count} Guilds");
                    
                });
                //Init Playing
                var playingGame = new DiscordGame();
                playingGame.Name = "Windows 95"; 
                await discord.UpdateStatusAsync(playingGame);
            };

            commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = cfgjson.CommandPrefix,
                EnableMentionPrefix = true
            });
            

            commands.RegisterCommands<MyCommands>();
            commands.SetHelpFormatter<SimpleHelpFormatter>();
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
        public struct ConfigJson
        {
            [JsonProperty("token")]
            public string Token { get; private set; }

            [JsonProperty("prefix")]
            public string CommandPrefix { get; private set; }
        }
    }

}
