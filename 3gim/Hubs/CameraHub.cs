using Microsoft.AspNetCore.SignalR;
using MySqlX.XDevAPI;

namespace _3gim.Hubs
{
    public class CameraHub : Hub
    {
        public async Task BeltCamera(string message)
        {
            await Clients.All.SendAsync("ReceiveVideo", message);
        }
        public async Task BeltCamera2(string message)
        {
            await Clients.All.SendAsync("ReceiveVideo2", message);
        }

        public async Task AICamera(string message)
        {
            await Clients.All.SendAsync("ReceiveAI", message);
        }
    }
}
