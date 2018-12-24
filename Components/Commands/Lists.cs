using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using FriendlyBot.Components.ListsSystem;
using FriendlyBot.Utils;

namespace FriendlyBot.Components.Commands
{
    public class Lists : ModuleBase<SocketCommandContext>
    {
        [Command("listscreate")]
        public Task CreateList(string listName)
        {
            ListsOperations.AddList(listName);
            return Task.CompletedTask;
        }

        [Command("listsdelete")]
        public Task RemoveList(string listName)
        {
            ListsOperations.RemoveList(listName);
            return Task.CompletedTask;
        }

        [Command("listsshow")]
        public async Task ShowLists()
        {
            var lists = ListsOperations.GetLists();
            var embed = new EmbedBuilder();
            embed.WithTitle("Lists");
            // foreach (var list in lists)
            // {
            // embed.AddField(list.Key, "");
            // }
            for (int i = 0; i < lists.Count; i++)
            {
                embed.AddField((i + 1).ToString() + ".", lists.ToList()[i].Key);
            }
            await Context.Channel.SendMessageAsync("", embed: embed);
        }

        [Command("listadd")]
        [Alias(new string[] { "la", "a" })]
        public Task AddToList(string listName, string item)
        {
            ListOperations.AddItem(listName, item);
            return Task.CompletedTask;
        }
        [Command("listadd")]
        [Alias(new string[] { "la", "a" })]
        public Task AddToList(string listName, string item, int pos)
        {
            ListOperations.AddItem(listName, item, pos);
            return Task.CompletedTask;
        }
        [Command("listadd")]
        [Alias(new string[] { "la", "a" })]
        public Task AddToList(string listName, string item, int pos, [Remainder]string note)
        {
            ListOperations.AddItem(listName, item, pos, note);
            return Task.CompletedTask;
        }
        [Command("listadd")]
        [Alias(new string[] { "la", "a" })]
        public Task AddToList(string listName, string item, [Remainder]string note)
        {
            ListOperations.AddItem(listName, item, note);
            return Task.CompletedTask;
        }

        [Command("listremovefirst")]
        [Alias(new string[] { "lrf", "rf", "rmf" })]
        public Task RemoveFirstFromList(string listName)
        {
            ListOperations.RemoveItem(listName, 0);
            return Task.CompletedTask;
        }
        [Command("listremovefirst")]
        [Alias(new string[] { "lrf", "rf", "rmf" })]
        public Task RemoveFirstFromList(string listName, int count)
        {
            for (int i = 0; i < count; i++)
                ListOperations.RemoveItem(listName, 0);
            return Task.CompletedTask;
        }

        [Command("listremove")]
        [Alias(new string[] { "r", "rm", "lr" })]
        public Task RemoveFromList(string listName, int pos)
        {
            ListOperations.RemoveItem(listName, pos - 1);
            return Task.CompletedTask;
        }
        [Command("listremove")]
        [Alias(new string[] { "r", "rm", "lr" })]
        public Task RemoveFromList(string listName, string item)
        {
            ListOperations.RemoveItem(listName, item);
            return Task.CompletedTask;
        }

        [Command("listposition")]
        [Alias(new string[] { "listpos", "lp", "p", "pos" })]
        public async Task GetPosition(string listName, string item)
        {
            int pos = ListOperations.GetItemPosition(listName, item);
            if (pos == 0)
                await Context.Channel.SendMessageAsync(Strings.GetString("WAITING_NO_ITEM"));
            else
                await Context.Channel.SendMessageAsync(pos.ToString());
        }

        [Command("listshow")]
        [Alias(new string[] { "ls", "s" })]
        public async Task ShowList(string listName)
        {
            var list = ListOperations.GetList(listName);
            var embed = new EmbedBuilder();
            embed.WithTitle(listName.First().ToString().ToUpper() + listName.Substring(1));
            for (int i = 0; i < list.Count; i++)
                embed.AddField((i + 1).ToString() + ". " + list[i].Item1, list[i].Item2);
            await Context.Channel.SendMessageAsync("", embed: embed);
        }

        [Command("listclear")]
        [Alias(new string[] { "c", "lc" })]
        public Task Clear(string listName)
        {
            ListOperations.ClearList(listName);
            return Task.CompletedTask;
        }
    }
}