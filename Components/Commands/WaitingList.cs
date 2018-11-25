using System.Threading.Tasks;
using Discord.Commands;
using FriendlyBot.Components;

namespace FriendlyBot.Components.Commands
{
    public class WaitingList : ModuleBase<SocketCommandContext>
    {
        [Command("add")]
        public Task AddToList([Remainder]string person)
        {
            WaitingListSystem.WaitingList.AddPerson(person);
            return Task.CompletedTask;
        }
        [Command("add")]
        public Task AddToList(uint pos, [Remainder]string person)
        {
            WaitingListSystem.WaitingList.AddPerson(person, pos);
            return Task.CompletedTask;
        }

        [Command("remove")]
        public Task RemoveFromList()
        {
            WaitingListSystem.WaitingList.RemovePerson(0);
            return Task.CompletedTask;
        }
        [Command("remove")]
        public Task RemoveFromList(uint pos)
        {
            WaitingListSystem.WaitingList.RemovePerson(pos);
            return Task.CompletedTask;
        }
        [Command("remove")]
        public Task RemoveFromList([Remainder]string person)
        {
            WaitingListSystem.WaitingList.RemovePerson(person);
            return Task.CompletedTask;
        }

        [Command("position")]
        public async Task GetPosition([Remainder]string person)
        {
            int pos = WaitingListSystem.WaitingList.GetPersonPosition(person);
            if (pos == -1)
                await Context.Channel.SendMessageAsync("That person doesn't appear in waiting list");
            else
                await Context.Channel.SendMessageAsync(pos.ToString());
        }
    }
}