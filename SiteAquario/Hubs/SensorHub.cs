using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SiteAquario.Data;

namespace SiteAquario.Hubs
{
    public class SensorHub : Hub
    {
        private readonly SiteAquarioContext _context;

        public SensorHub(SiteAquarioContext context)
        {
            _context = context;
        }

        public async Task SendData()
        {
            var data = await _context.ValoresSensor.ToListAsync();
            await Clients.All.SendAsync("ReceiveData", data);
        }
    }
}
