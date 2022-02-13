using System;
using System.Drawing;
using System.Threading.Tasks;
using Discord;
using Discord.Logging;
using Discord.Net.Converters;
using Discord.Rest;
using Discord.WebSocket;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace self_bit
{
    internal class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;

        public async Task MainAsync()
        {

            //const int SW_HIDE = 0;
            const int SW_SHOW = 5;
            var handle = GetConsoleWindow();
            //ShowWindow(handle, SW_HIDE);
            ShowWindow(handle, SW_SHOW);

            _client = new DiscordSocketClient();
            _client.MessageReceived += CommandHandler;
            _client.Log += Log;

            var token = "OTQyMTYyOTkxOTU5NDA0NTc0.YggfqQ.W4-RJ9zSFncaeGsmCGRNOv9Ztpw";



            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();


            await Task.Delay(-1);
        }
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
        private Task CommandHandler(SocketMessage message)
        {
            //acctuall commands here
            if (message.Content == "!ping")
            {
                message.Channel.SendMessageAsync($@"pong, {message.Author.Mention}");
            }

            if (message.Content == "!help")
            {
                var help = new EmbedBuilder();
                help.WithTitle("Help");
                help.WithDescription("");
                help.WithColor(Discord.Color.Teal);
                message.Channel.SendMessageAsync("", false, help.Build());
            }

            if(message.Content == "!urmom")
            {
                var urmom = new EmbedBuilder();
                urmom.WithTitle("uwu");
                urmom.WithDescription("i wuv u");
                message.Channel.SendMessageAsync("", false, urmom.Build());
            }

            if(message.Content == "+userinfo")
            {
                var info = new EmbedBuilder();
                info.WithTitle("+userinfo requested by " + message.Author.Username);
                info.AddField(name: "Username", value: message.Author.Username, inline: true);
                info.AddField(name: "Discriminator", value: message.Author.Discriminator, inline: true);
                info.AddField(name: "Creation Date", value: message.Author.CreatedAt, inline: true);
                info.AddField(name: "ID", value: message.Author.Id, inline: true);
                info.AddField(name: "Is a bot?", value: message.Author.IsBot, inline: true);
                info.AddField(name: "Flags", value: message.Author.PublicFlags, inline : true);
                info.AddField(name: "Current Status", value: message.Author.Status, inline: true);
                info.WithFooter("command sent at " + message.Timestamp.DateTime);
                info.WithThumbnailUrl(message.Author.GetAvatarUrl());
                message.Channel.SendMessageAsync("", false, info.Build());
            }

            if (message.Content == "!status dnd")
            {
                _client.SetStatusAsync(UserStatus.DoNotDisturb);
                message.Channel.SendMessageAsync("Changed status :3");
            }

            if (message.Content == "!status afk")
            {
                _client.SetStatusAsync(UserStatus.AFK);
                message.Channel.SendMessageAsync("Changed status :3");
            }

            if (message.Content == "!status idle")
            {
                _client.SetStatusAsync(UserStatus.Idle);
                message.Channel.SendMessageAsync("Changed status :3");
            }

            if (message.Content == "!status online")
            {
                _client.SetStatusAsync(UserStatus.Online);
                message.Channel.SendMessageAsync("Changed status :3");
            }

            if (message.Content == "!status offline")
            {
                _client.SetStatusAsync(UserStatus.Offline);
                message.Channel.SendMessageAsync("Changed status :3");
            }

            if (message.Content.StartsWith("!status custom"))
            {
                try
                {
                    string owo = message.Content.Split(new[] { "!status custom" }, StringSplitOptions.None)[1];
                    _client.SetActivityAsync(new Game(owo, ActivityType.Playing, ActivityProperties.None));
                    message.Channel.SendMessageAsync("Changed status :3");
                }
                catch (Exception ex)
                {
                    message.Channel.SendMessageAsync("uwu" + ex);
                }
            }

            return Task.CompletedTask;
        }
    }
}
