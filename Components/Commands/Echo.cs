using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using FriendlyBot.Utils;
using FriendlyBot.Components.EchoSystem;

namespace FriendlyBot.Components.Commands
{
    public class Echo : ModuleBase<SocketCommandContext>
    {
        [Command("echo")]
        public async Task EchoMessage([Remainder] string message)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle(Strings.GetString("ECHO_TITLE_&USERNAME", Context.User.Username));
            embed.WithDescription(message);
            embed.WithColor(255, 0, 0);

            await Context.Channel.SendMessageAsync("", embed: embed);
        }

        // [Command("autoecho")]
        // public async Task ToggleAutoEcho()
        // {
        //     AutoEcho.echoData.echoEnabled
        // }
    }
}