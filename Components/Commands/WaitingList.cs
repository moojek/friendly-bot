using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using FriendlyBot.Components;

namespace FriendlyBot.Components.Commands
{
    public class WaitingList : ModuleBase<SocketCommandContext>
    {
        [Command("add")]
        public Task AddToList(string person)
        {
            ListsSystem.WaitingList.AddPerson(person);
            return Task.CompletedTask;
        }
        [Command("add")]
        public Task AddToList(string person, uint pos)
        {
            ListsSystem.WaitingList.AddPerson(person, pos - 1);
            return Task.CompletedTask;
        }
        [Command("add")]
        public Task AddToList(string person, [Remainder]string note)
        {
            ListsSystem.WaitingList.AddPerson(person, note);
            return Task.CompletedTask;
        }
        [Command("add")]
        public Task AddToList(string person, uint pos, [Remainder]string note)
        {
            ListsSystem.WaitingList.AddPerson(person, pos, note);
            return Task.CompletedTask;
        }

        [Command("remove")]
        public Task RemoveFromList()
        {
            ListsSystem.WaitingList.RemovePerson(0);
            return Task.CompletedTask;
        }
        [Command("remove")]
        public Task RemoveFromList(uint pos)
        {
            ListsSystem.WaitingList.RemovePerson(pos - 1);
            return Task.CompletedTask;
        }
        [Command("remove")]
        public Task RemoveFromList(string person)
        {
            ListsSystem.WaitingList.RemovePerson(person);
            return Task.CompletedTask;
        }

        [Command("position")]
        public async Task GetPosition([Remainder]string person)
        {
            int pos = ListsSystem.WaitingList.GetPersonPosition(person);
            if (pos == 0)
                await Context.Channel.SendMessageAsync("That person doesn't appear in waiting list");
            else
                await Context.Channel.SendMessageAsync(pos.ToString());
        }

        [Command("list")]
        public async Task List()
        {
            var list = ListsSystem.WaitingList.GetList();
            // var text = String.Join('\n', list);
            var embed = new EmbedBuilder();
            for (int i = 0; i < list.Count; i++)
                embed.AddField((i+1)+". " + i.ToString() + list[i].Key, list[i].Value);
            embed.WithTitle("Waiting list");
            // embed.WithDescription(text);
            await Context.Channel.SendMessageAsync("", embed: embed);
        }
    }
}