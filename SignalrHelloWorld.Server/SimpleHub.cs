using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalrHelloWorld.Server
{
    public class SimpleHub : Hub
    {
        public async Task Ping() =>
            await Clients.Caller.SendAsync("Pong");
    }
}