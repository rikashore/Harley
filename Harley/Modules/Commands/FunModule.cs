using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Harley.Modules.Commands
{
    public class FunModule : ModuleBase<SocketCommandContext>
    {
        [Command("rps"), Summary("Play Rock Paper Scissors with Harley")]
        public async Task RpsAsync()
        {
            var rockButton = new ButtonBuilder()
                .WithStyle(ButtonStyle.Primary)
                .WithLabel("Rock")
                .WithCustomId("rock_button");
            
            var paperButton = new ButtonBuilder()
                .WithStyle(ButtonStyle.Success)
                .WithLabel("Paper")
                .WithCustomId("paper_button");
            
            var scissorButton = new ButtonBuilder()
                .WithStyle(ButtonStyle.Secondary)
                .WithLabel("Scissors")
                .WithCustomId("scissor_button");

            var component = new ComponentBuilder()
                .WithButton(rockButton)
                .WithButton(paperButton)
                .WithButton(scissorButton)
                .Build();

            await ReplyAsync("Choose!", component: component);
        } 
    }
}