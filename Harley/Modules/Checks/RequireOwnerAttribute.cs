using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Harley.Modules.Checks
{
    public class RequireOwnersAttribute : RequireOwnerAttribute
    {
        public override string ErrorMessage { get; set; } = "This command can only be used by the owners of the bot";

        // TODO make this use me and quin's Id
        public override async Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context,
            CommandInfo command, IServiceProvider services)
        {
            if (context.Client.TokenType == TokenType.Bot)
            {
                var application = await context.Client.GetApplicationInfoAsync().ConfigureAwait(false);
                if (context.User.Id != application.Owner.Id)
                    return PreconditionResult.FromError(ErrorMessage ??
                                                        "Command can only be run by the owner of the bot.");
                return PreconditionResult.FromSuccess();
            }

            return PreconditionResult.FromError(
                $"{nameof(RequireOwnerAttribute)} is not supported by this {nameof(TokenType)}.");
        }
    }
}