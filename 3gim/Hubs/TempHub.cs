using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualBasic;
using _3gim.Controllers;
using _3gim.Models;
using _3gim.Data;

namespace _3gim.Hubs
{
    public class TempHub : Hub
    {
        private readonly _3gimDbContext _dbContext;
        public TempHub(_3gimDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SendTemp(float temp)
        {
            await Clients.All.SendAsync("ReceiveTemp", temp);
        }

        public async Task SaveTemp(float temp, string date, string time)
        {
            Console.WriteLine(temp);
            Temperature temperature = new Temperature();
            temperature.Date = date;
            temperature.Time = time;
            temperature.Temp = temp;

            _dbContext.Temperature.Add(temperature);

            _dbContext.SaveChanges();
        }

    }
}
