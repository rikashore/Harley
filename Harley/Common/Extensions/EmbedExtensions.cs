using Discord;
using Harley.Common.Utilities;

namespace Harley.Common.Extensions
{
    public static class EmbedExtensions
    {
        public static EmbedBuilder WithHarleyColor(this EmbedBuilder builder) 
            => builder.WithColor(HarleyGlobals.HarleyColor);
    }
}