using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualBasic;

namespace _3gim.Hubs
{
    public class TempHub : Hub
    {

        public async Task SendTemp(float temp)
        {
            await Clients.All.SendAsync("ReceiveTemp", temp);
        }   
    }
}
