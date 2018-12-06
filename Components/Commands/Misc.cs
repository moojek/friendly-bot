using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System;
using Discord.WebSocket;
using System.Collections.Generic;

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

        [Command("textall")]
        public async Task TextAll([Remainder]string message)
        {
            var users = Context.Guild.Users;
            foreach (var user in users)
            {
                if (!user.IsBot)
                    await user.SendMessageAsync(message);
            }
        }

        [Command("mostroles")]
        public async Task GetUserWithMostRoles()
        {
            var users = Context.Guild.Users;
            // Tuple<SocketGuildUser,int> max = new Tuple<SocketGuildUser, int>(null, 0);
            int max = 0;
            List<SocketGuildUser> max_users = new List<SocketGuildUser>();
            foreach (var user in users)
            {
                var roles = user.Roles;
                if (roles.Count > max)
                {
                    max = roles.Count;
                    max_users.Clear();
                    max_users.Add(user);
                }
            }
            foreach (var user in max_users)
            {
                await Context.Channel.SendMessageAsync(user.Nickname);
            }
        }
    }
}