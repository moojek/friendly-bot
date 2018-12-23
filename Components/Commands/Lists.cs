using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using FriendlyBot.Components;
using FriendlyBot.Utils;

namespace FriendlyBot.Components.Commands
{
    public class Lists : ModuleBase<SocketCommandContext>
    {
        [Command("listadd")]
        [Alias(new string[] { "la", "a" })]
        public Task AddToList(string person)
        {
            ListsSystem.WaitingList.AddPerson(person);
            return Task.CompletedTask;
        }
        [Command("listadd")]
        [Alias(new string[] { "la", "a" })]
        public Task AddToList(string person, uint pos)
        {
            ListsSystem.WaitingList.AddPerson(person, pos - 1);
            return Task.CompletedTask;
        }
        [Command("listadd")]
        [Alias(new string[] { "la", "a" })]
        public Task AddToList(string person, uint pos, [Remainder]string note)
        {
            ListsSystem.WaitingList.AddPerson(person, pos, note);
            return Task.CompletedTask;
        }
        [Command("listadd")]
        [Alias(new string[] { "la", "a" })]
        public Task AddToList(string person, [Remainder]string note)
        {
            ListsSystem.WaitingList.AddPerson(person, note);
            return Task.CompletedTask;
        }

        [Command("listremovefirst")]
        [Alias(new string[] { "lrf", "rf", "rmf" })]
        public Task RemoveFirstFromList()
        {
            ListsSystem.WaitingList.RemovePerson(0);
            return Task.CompletedTask;
        }
        [Command("listremovefirst")]
        [Alias(new string[] { "lrf", "rf", "rmf" })]
        public Task RemoveFirstFromList(uint count)
        {
            for (int i = 0; i < count; i++)
                ListsSystem.WaitingList.RemovePerson(0);
            return Task.CompletedTask;
        }

        [Command("listremove")]
        [Alias(new string[] { "r", "rm", "lr" })]
        public Task RemoveFromList(uint pos)
        {
            ListsSystem.WaitingList.RemovePerson(pos - 1);
            return Task.CompletedTask;
        }
        [Command("listremove")]
        [Alias(new string[] { "r", "rm", "lr" })]
        public Task RemoveFromList(string person)
        {
            ListsSystem.WaitingList.RemovePerson(person);
            return Task.CompletedTask;
        }

        [Command("listposition")]
        [Alias(new string[] { "listpos", "lp", "p", "pos" })]
        public async Task GetPosition(string person)
        {
            int pos = ListsSystem.WaitingList.GetPersonPosition(person);
            if (pos == 0)
                await Context.Channel.SendMessageAsync(Strings.GetString("WAITING_NO_PERSON"));
            else
                await Context.Channel.SendMessageAsync(pos.ToString());
        }

        [Command("listshow")]
        [Alias(new string[] { "ls", "s" })]
        public async Task List()
        {
            var list = ListsSystem.WaitingList.GetList();
            // var text = String.Join('\n', list);
            var embed = new EmbedBuilder();
            embed.WithTitle("Waiting list");
            for (int i = 0; i < list.Count; i++)
                embed.AddField((i + 1) + ". " + i.ToString() + list[i].Key, list[i].Value);
            // embed.WithDescription(text);
            await Context.Channel.SendMessageAsync("", embed: embed);
        }

        [Command("listclear")]
        [Alias(new string[] { "c", "lc" })]
        public Task Clear()
        {
            ListsSystem.WaitingList.ClearList();
            return Task.CompletedTask;
        }
    }
}