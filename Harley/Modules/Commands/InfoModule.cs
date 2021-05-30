using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Harley.Common.Utilities;

namespace Harley.Modules.Commands
{
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        [Command("info")]
        public async Task InfoAsync()
        {
            var infoButton = new ButtonBuilder()
                .WithLabel("Info")
                .WithStyle(ButtonStyle.Primary)
                .WithCustomId("main_info");

            var nameButton = new ButtonBuilder()
                .WithLabel("The name?")
                .WithStyle(ButtonStyle.Secondary)
                .WithCustomId("why_name");

            var componentBuilder = new ComponentBuilder()
                .WithButton(infoButton)
                .WithButton(nameButton)
                .Build();

            await ReplyAsync(embed: InfoEmbeds.MainInfoEmbed, component: componentBuilder);
        }
    }
}