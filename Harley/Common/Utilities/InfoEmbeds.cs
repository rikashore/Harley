using Discord;
using Harley.Common.Extensions;

namespace Harley.Common.Utilities
{
    public static class InfoEmbeds
    {
        public static readonly Embed MainInfoEmbed = new EmbedBuilder()
            .WithTitle("Harley, the bot that uses Labs")
            .WithDescription("A bot built to showcase Discord.Net-Labs")
            .WithHarleyColor()
            .AddField("Library", HarleyGlobals.LibraryRepoMarkdown)
            .AddField("Made by", "shift-eleven#7304")
            .Build();

        public static readonly Embed NameInfoEmbed = new EmbedBuilder()
            .WithTitle("Why the name Harley?")
            .WithDescription("Well because this dude named Quin made labs, and the first thing in my mind was Harley Quinn, so there you go")
            .WithHarleyColor()
            .Build();
    }
}