using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System;
using Discord.WebSocket;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Drawing;
using FriendlyBot.Utils;

namespace FriendlyBot.Components.Commands
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("echo")]
        public async Task Echo([Remainder] string message)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle(Strings.GetString("ECHO_TITLE_&USERNAME",Context.User.Username));
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

        [Command("fromjoin")]
        public async Task GetTimeFromJoin(IRole role)
        {
            var currentTime = DateTimeOffset.Now;
            foreach (var user in ((SocketRole)role).Members)
            {
                var joinedTime = user.JoinedAt;
                string time = (currentTime - joinedTime).Value.Days.ToString();
                // string name = user.Nickname == null ? user.Username : user.Nickname;
                await Context.Channel.SendMessageAsync((user.Nickname ?? user.Username) + ": " + time);
            }
            await Context.Channel.SendMessageAsync("-------");
        }
        [Command("fromjoin")]
        public async Task GetTimeFromJoin(IUser user)
        {
            var currentTime = DateTimeOffset.Now;
            var joinedTime = ((SocketGuildUser)user).JoinedAt;
            string time = (currentTime - joinedTime).Value.Days.ToString();
            await Context.Channel.SendMessageAsync((((SocketGuildUser)user).Nickname ?? user.Username) + ": " + time);
            await Context.Channel.SendMessageAsync("-------");
        }
        [Command("fromjoin")]
        public async Task GetTimeFromJoin()
        {
            var currentTime = DateTimeOffset.Now;
            foreach (var user in Context.Guild.Users)
            {
                var joinedTime = user.JoinedAt;
                string time = (currentTime - joinedTime).Value.Days.ToString();
                // string name = user.Nickname == null ? user.Username : user.Nickname;
                await Context.Channel.SendMessageAsync((user.Nickname ?? user.Username) + ": " + time);
            }
            await Context.Channel.SendMessageAsync("-------");
        }

        // [Command("pruneasleep")]

        [Command("getmessage")]
        public async Task SendMessageStringAsFile(ulong ID)
        {
            var message = Context.Channel.GetMessageAsync(ID);
            var msgString = message.Result.Content;
            string filePath = "Temp/" + ID.ToString();
            File.WriteAllText(filePath, msgString);
            await Context.Channel.SendFileAsync(filePath);
            File.Delete(filePath);
        }
        [Command("getmessage")]
        public async Task SendMessageStringAsFile(ulong ID, IChannel channel)
        {
            var message = ((ISocketMessageChannel)channel).GetMessageAsync(ID);
            var msgString = message.Result.Content;
            string filePath = "Temp/" + ID.ToString();
            File.WriteAllText(filePath, msgString);
            await Context.Channel.SendFileAsync(filePath);
            File.Delete(filePath);
        }

        [Command("embed")]
        public async Task EmbedString(string clr,[Remainder]string text)
        {
            var tempColour = System.Drawing.Color.FromName(clr);
            var colour = new Discord.Color(tempColour.R, tempColour.G, tempColour.B);
            var embed = new EmbedBuilder();
            embed.WithDescription(text);
            embed.WithColor(colour);
            await Context.Channel.SendMessageAsync("", embed: embed);
        }
    }
}