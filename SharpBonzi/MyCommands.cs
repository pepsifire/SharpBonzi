﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Diagnostics;

namespace SharpBonzi
{
    public class MyCommands
    {
        
        static readonly List<String> Jokes = new List<String> {
            "What do you call a cow that can't give milk? An utter failure!",
            "What's black when clean and white when dirty? A Blackboard!",
            "Why did the scientist install a knocker on his door? To win the Nobel prize!",
            "Who earns a living by driving his customers away? A taxi driver!",
            "What question can never be answered by \"yes\"? Are you asleep?",
            "Why do they call HTML hyper text? Too much Java!",
            "When is the best time to go to bed? When the bed won't come to you!",
            "Why couldn't the leopard escape from the zoo? Cause he was always spotted!",
            "What rises and waves all day? A flag!",
            "Where do dogs go when they've lost their tails? To the retailer!",
            "What do you call the life-story of a car? An autobiography!" };
        static readonly List<String> WisdomList = new List<String>
        {
            "https://i.imgur.com/CeNVUQD.jpg",
            "https://i.imgur.com/28UM8TQ.png",
            "https://i.imgur.com/Q2pH2PX.png"

        };
        public Stopwatch AppUptime = Stopwatch.StartNew();

        [Command("hi"), Description("Greetings!")]
        public async Task Hi(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync($"Hello, {ctx.User.Mention}");
            ConsoleLogger(ctx, "Said hello");
            

        }
        [Command("joke"), Description("Tells you a (terrible) joke")]
        public async Task Joke(CommandContext ctx)
        {
            var Rnd = new Random();
            var index = Rnd.Next(0, Jokes.Count);
            await ctx.RespondAsync($"{Jokes[index]}");
            ConsoleLogger(ctx, "Told a joke");
        }
        [Command("info"), Description("Tells you about Bonzi Buddy")]
        public async Task Info(CommandContext ctx)
        {
            var rnd = new Random();
            await ctx.TriggerTypingAsync();
            var embed = new DiscordEmbedBuilder
            {
                Title = "Bonzi Buddy",
                Description = $"Bonzi Buddy in C# run by {ctx.Client.CurrentApplication.Owner.Mention}"
                
                
            };
            embed.WithFooter(ctx.Client.CurrentUser.Username, ctx.Client.CurrentUser.AvatarUrl)
                .AddField("Version", $"0.{rnd.Next(1,3)}.{rnd.Next(10,20)} pre-alpha private release steam greenlight early access (random ints are great btw)")
                .AddField("Looking good", "Mr Worldwide");


            embed.WithImageUrl(ctx.Client.CurrentUser.AvatarUrl);

            await ctx.RespondAsync(embed: embed.Build());
            ConsoleLogger(ctx, "Told about myself");
        }
        [Command("say"), Description("Repeats the message")]
        public async Task Say(CommandContext ctx, string word)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync(word);
        }
        [Command("wisdom"), Description("Ask Bonzi for useful tips")]
        public async Task Wisdom(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            var Rnd = new Random();
            var index = Rnd.Next(0, WisdomList.Count);
            var embed = new DiscordEmbedBuilder
            {
                Title = "Wisdom",
                ImageUrl = WisdomList[index]
            };
            await ctx.RespondAsync(embed: embed.Build());
            ConsoleLogger(ctx, "Wisdom served");
        }
        [Command("dumptoken"), Description("Dumps the secret token")]
        public async Task DumpToken(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            if (Permissions.CheckOwner(ctx.Message.Author))
            {
                await ctx.RespondAsync(":white_check_mark: lols :white_check_mark:");
            }
            else
            {
                await ctx.RespondAsync(":x: You don't have required permissions to do this! :x:");
            }
        }
        [Command("uptime"), Description("Show uptime of the bot")]
        public async Task Uptime(CommandContext ctx)
        {
            var time = AppUptime.Elapsed;
            await ctx.RespondAsync($"Bot uptime: {time.Days} days, {time.Hours} hours, {time.Minutes} minutes, {time.Seconds} seconds");
        }
        
        public void ConsoleLogger(CommandContext ctx, string input)
        {
            Console.WriteLine($"{ctx.Message.Author.Username} in {ctx.Message.Channel.Guild.Name} | {input}");
        }
    }

       
}