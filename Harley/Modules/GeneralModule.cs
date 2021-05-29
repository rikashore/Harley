using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Harley.Modules
{
    public class GeneralModule : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task PingAsync()
        {
            await ReplyAsync("pong!");
        }
        
        [Command("url-button")]
        public async Task UrlButtonAsync()
        {
            var builder = new ComponentBuilder().WithButton("Hello!", customId: null, ButtonStyle.Link, emote: null,
                url: "https://github.com/shift-eleven/Alexandra", disabled: false, row: 0);

            await Context.Channel.SendMessageAsync("Buttons!", component: builder.Build());
        }
        
        [Command("simple-button")]
        public async Task SimpleButtonAsync()
        {
            var builder = new ComponentBuilder().WithButton("Hello!","id_1", ButtonStyle.Primary);
            await Context.Channel.SendMessageAsync("Test buttons!", component: builder.Build());
        }
        
        // We handle these buttons in our event in the command handler
        [Command("custom-id")]
        public async Task CustomButtonResponseAsync()
        {
            var successButton = new ButtonBuilder()
                .WithLabel("Press this")
                .WithStyle(ButtonStyle.Success)
                .WithCustomId("custom_success_button");
            
            var dangerButton = new ButtonBuilder()
                .WithLabel("Don't press this")
                .WithStyle(ButtonStyle.Danger)
                .WithCustomId("custom_danger_button");

            var component = new ComponentBuilder()
                .WithButton(successButton)
                .WithButton(dangerButton)
                .Build();

            await Context.Channel.SendMessageAsync("Press the right button", component: component);
        }

        
        // Weird feeling rows are bugged, but the buttons work as intended
        [Command("multi-button")]
        public async Task MultiButtonAsync()
        {
            var successButton = new ButtonBuilder()
                .WithLabel("success")
                .WithCustomId("success_button")
                .WithStyle(ButtonStyle.Success);

            var primaryButton = new ButtonBuilder()
                .WithLabel("primary")
                .WithCustomId("primary_button")
                .WithStyle(ButtonStyle.Primary);

            var secondaryButton = new ButtonBuilder()
                .WithLabel("secondary")
                .WithCustomId("secondary_button")
                .WithStyle(ButtonStyle.Secondary);
            
            var dangerButton = new ButtonBuilder()
                .WithLabel("danger!")
                .WithCustomId("danger_button")
                .WithStyle(ButtonStyle.Danger);

            var component = new ComponentBuilder()
                .WithButton(successButton, 0)
                .WithButton(primaryButton, 1)
                .WithButton(secondaryButton, 2)
                .WithButton(dangerButton, 3)
                .Build();

            await Context.Channel.SendMessageAsync("Multi-Buttons!", component: component);
        }
    }
}