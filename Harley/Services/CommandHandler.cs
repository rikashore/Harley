using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Harley.Common.Utilities;
using Microsoft.Extensions.Configuration;

namespace Harley.Services
{
    public class CommandHandler : InitializedService
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _service;
        private readonly IConfiguration _config;

        public CommandHandler(IServiceProvider provider, DiscordSocketClient client, CommandService service, IConfiguration config)
        {
            _provider = provider;
            _client = client;
            _service = service;
            _config = config;
        }

        public override async Task InitializeAsync(CancellationToken cancellationToken)
        {
            _client.MessageReceived += OnMessageReceived;
            
            _client.InteractionCreated += BaseInteractionHandler;
            _client.InteractionCreated += InfoInteractionHandler;
            
            _service.CommandExecuted += OnCommandExecuted;
            
            await _service.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }

        private async Task InfoInteractionHandler(SocketInteraction arg)
        {
            if (arg.Type == InteractionType.MessageComponent)
            {
                var componentArg = (SocketMessageComponent) arg;
                if (componentArg.Data.CustomId == "main_info" && componentArg.Message.Embeds.First() == InfoEmbeds.MainInfoEmbed)
                    return;
                if (componentArg.Data.CustomId == "why_name" && componentArg.Message.Embeds.First() == InfoEmbeds.NameInfoEmbed)
                    return;
                
                switch (componentArg.Data.CustomId)
                {
                    case "main_info":
                    {
                        if (componentArg.Message is IUserMessage message)
                            await message.ModifyAsync(x => x.Embed = InfoEmbeds.MainInfoEmbed);
                        break;
                    }
                    case "why_name":
                    {
                        if (componentArg.Message is IUserMessage message)
                            await message.ModifyAsync(x => x.Embed = InfoEmbeds.NameInfoEmbed);
                        break;
                    }
                }
            }
        }

        private async Task BaseInteractionHandler(SocketInteraction arg)
        {
            if (arg.Type == InteractionType.MessageComponent)
            {
                var componentArg = (SocketMessageComponent) arg;
                switch (componentArg.Data.CustomId)
                {
                    case "custom_success_button":
                        await componentArg.RespondAsync("You pressed the right button!", type: InteractionResponseType.UpdateMessage);
                        break;
                    case "custom_danger_button":
                        await componentArg.RespondAsync("Oh no! You pressed the wrong button", type: InteractionResponseType.UpdateMessage);
                        break;
                }
            }
        }

        private async Task OnMessageReceived(SocketMessage arg)
        {
            if (arg is not SocketUserMessage {Source: MessageSource.User} message) return;

            var argPos = 0;
            if (!message.HasStringPrefix(_config["discord:prefix"], ref argPos) && !message.HasMentionPrefix(_client.CurrentUser, ref argPos)) return;

            var context = new SocketCommandContext(_client, message);
            await _service.ExecuteAsync(context, argPos, _provider);
        }

        private async Task OnCommandExecuted(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            if (command.IsSpecified && !result.IsSuccess) await context.Channel.SendMessageAsync($"Error: {result}");
        }
    }
}