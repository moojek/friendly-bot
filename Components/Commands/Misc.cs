using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace FriendlyBot.Components.Commands
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("echo")]
        public async Task Echo([Remainder] string message)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle($"{Context.User.Username}'s echoed message");
            embed.WithDescription(message);
            embed.WithColor(255, 0, 0);

            await Context.Channel.SendMessageAsync("", embed: embed);
        }
    }
}