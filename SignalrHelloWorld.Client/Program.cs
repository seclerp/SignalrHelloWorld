using System;
using Microsoft.AspNetCore.SignalR.Client;
using SignalrHelloWorld.Shared;

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:80/simple")
    .Build();

connection.On("Pong", () =>
{
    Console.WriteLine("Pong Received");
});

connection.On("Hello", (IntervalSendModel model) =>
{
    Console.WriteLine($"[{model.TimeFormatted}]: {model.Message}");
});

await connection.StartAsync();
await connection.SendAsync("Ping");

// Block until connection will be lost
while (connection.State != HubConnectionState.Disconnected)
{
}