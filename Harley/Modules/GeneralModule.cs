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
            await Context.Channel.SendMessageAsync("pong!");
        }

        // Currently doesn't work
        [Command("button")]
        public async Task ButtonAsync()
        {
            var builder = new ComponentBuilder().WithButton("Hello!", ButtonStyle.Link, emote: null, customId: "id_2",
                url: "https://github.com/shift-eleven/Alexandra", disabled: false, row: 0);

            await Context.Channel.SendMessageAsync("Buttons!", component: builder.Build());
        }
        
        [Command("button2")]
        public async Task SecondButtonAsync()
        {
            var builder = new ComponentBuilder().WithButton("Hello!", ButtonStyle.Primary, customId: "id_1");
            await Context.Channel.SendMessageAsync("Test buttons!", component: builder.Build());
        }
    }
}