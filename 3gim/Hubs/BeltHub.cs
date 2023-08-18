using Microsoft.AspNetCore.SignalR;

namespace _3gim.Hubs
{
    public class BeltHub : Hub
    {
        public async Task BeltStatus(string belt)
        {
            await Clients.All.SendAsync("ReceiveBelt", belt);
        }
    }
}
