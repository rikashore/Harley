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
        // Necessary stuff (config and discord related)
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _service;
        private readonly IConfiguration _config;
        
        // Other stuff I want to inject
        private readonly RpsService _rpsService;

        public CommandHandler(IServiceProvider provider, DiscordSocketClient client, CommandService service, IConfiguration config, RpsService rpsService)
        {
            _provider = provider;
            _client = client;
            _service = service;
            _config = config;
            _rpsService = rpsService;
        }

        public override async Task InitializeAsync(CancellationToken cancellationToken)
        {
            _client.MessageReceived += OnMessageReceived;
            
            // Keeping single responsibility pattern by hooking the event multiple times
            _client.InteractionCreated += BaseInteractionHandler;
            _client.InteractionCreated += InfoInteractionHandler;
            _client.InteractionCreated += RpsInteractionHandler;
            
            _service.CommandExecuted += OnCommandExecuted;
            
            await _service.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }

        // TODO Figure out proper modifying of original message
        private async Task RpsInteractionHandler(SocketInteraction arg)
        {
            if (arg.Type == InteractionType.MessageComponent)
            {
                var componentArg = (SocketMessageComponent) arg;

                string playerChoice;

                switch (componentArg.Data.CustomId)
                {
                    case "rock_button":
                        playerChoice = "rock";
                        break;
                    case "paper_button":
                        playerChoice = "paper";
                        break;
                    case "scissor_button":
                        playerChoice = "scissor";
                        break;
                    default:
                        return;
                }
                
                var computerChoice = _rpsService.GetChoice();
                var winner = _rpsService.GetWinner(playerChoice, computerChoice);
                
                switch (winner)
                {
                    case 0:
                    {
                        await componentArg.RespondAsync("The computer wins!",
                            type: InteractionResponseType.ChannelMessageWithSource);
                        
                        if (componentArg.Message is SocketUserMessage message)
                            await message.ModifyAsync(x => x.Components = new ComponentBuilder().Build());
                        
                        break;
                    }
                    case 1:
                    {
                        await componentArg.RespondAsync("You win!",
                            type: InteractionResponseType.ChannelMessageWithSource);
                        
                        if (componentArg.Message is SocketUserMessage message)
                            await message.ModifyAsync(x => x.Components = new ComponentBuilder().Build());
                        
                        break;
                    }
                    case 2:
                    {
                        await componentArg.RespondAsync("A draw!",
                            type: InteractionResponseType.ChannelMessageWithSource);
                        
                        if (componentArg.Message is SocketUserMessage message)
                            await message.ModifyAsync(x => x.Components = new ComponentBuilder().Build());
                        
                        break;
                    }
                }
            }
        }

        private async Task InfoInteractionHandler(SocketInteraction arg)
        {
            if (arg.Type == InteractionType.MessageComponent)
            {
                var componentArg = (SocketMessageComponent) arg;
                switch (componentArg.Data.CustomId)
                {
                    case "main_info" when componentArg.Message.Embeds.First() == InfoEmbeds.MainInfoEmbed:
                    case "why_name" when componentArg.Message.Embeds.First() == InfoEmbeds.NameInfoEmbed:
                        return;
                    default:
                    {
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