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
        [Command("listadd")]
        [Alias(new string[] { "la", "a" })]
        public void AddToList(string listName, string item)
        {
            ListOperations.AddItem(listName, item);
        }
        [Command("listadd")]
        [Alias(new string[] { "la", "a" })]
        public void AddToList(string listName, string item, int pos)
        {
            ListOperations.AddItem(listName, item, pos);
        }
        [Command("listadd")]
        [Alias(new string[] { "la", "a" })]
        public void AddToList(string listName, string item, int pos, [Remainder]string note)
        {
            ListOperations.AddItem(listName, item, pos, note);
        }
        [Command("listadd")]
        [Alias(new string[] { "la", "a" })]
        public void AddToList(string listName, string item, [Remainder]string note)
        {
            ListOperations.AddItem(listName, item, note);
        }

        [Command("listremovefirst")]
        [Alias(new string[] { "lrf", "rf", "rmf" })]
        public void RemoveFirstFromList(string listName)
        {
            ListOperations.RemoveItem(listName, 0);
        }
        [Command("listremovefirst")]
        [Alias(new string[] { "lrf", "rf", "rmf" })]
        public void RemoveFirstFromList(string listName, int count)
        {
            for (int i = 0; i < count; i++)
                ListOperations.RemoveItem(listName, 0);
        }

        [Command("listremove")]
        [Alias(new string[] { "r", "rm", "lr" })]
        public void RemoveFromList(string listName, int pos)
        {
            ListOperations.RemoveItem(listName, pos - 1);
        }
        [Command("listremove")]
        [Alias(new string[] { "r", "rm", "lr" })]
        public void RemoveFromList(string listName, string item)
        {
            ListOperations.RemoveItem(listName, item);
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
        public async Task List(string listName)
        {
            var list = ListOperations.GetList(listName);
            var embed = new EmbedBuilder();
            embed.WithTitle(listName.First().ToString().ToUpper() + listName.Substring(1));
            for (int i = 0; i < list.Count; i++)
                embed.AddField((i + 1).ToString() + ". " + list[i].Item1, list[i].Item2);
            // embed.WithDescription(text);
            await Context.Channel.SendMessageAsync("", embed: embed);
        }

        [Command("listclear")]
        [Alias(new string[] { "c", "lc" })]
        public void Clear(string listName)
        {
            ListOperations.ClearList(listName);
        }
    }
}