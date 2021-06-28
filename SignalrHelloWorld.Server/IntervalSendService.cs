using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using SignalrHelloWorld.Shared;

namespace SignalrHelloWorld.Server
{
    public class IntervalSendService : BackgroundService
    {
        private readonly IHubContext<SimpleHub> _context;
        private readonly IHostApplicationLifetime _lifetime;

        public IntervalSendService(IHubContext<SimpleHub> context, IHostApplicationLifetime lifetime)
        {
            _context = context;
            _lifetime = lifetime;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken) =>
            Task.Run(async () =>
            {
                while (!_lifetime.ApplicationStopping.IsCancellationRequested)
                {
                    await Task.Delay(2000, stoppingToken);
                    await _context.Clients.All.SendAsync("Hello", new IntervalSendModel
                    {
                        Message = $"Hello, client",
                        TimeFormatted = DateTime.UtcNow.ToString("f")
                    }, stoppingToken);
                }
            }, stoppingToken);
    }
}