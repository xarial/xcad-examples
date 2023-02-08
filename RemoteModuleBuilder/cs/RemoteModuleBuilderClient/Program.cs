using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder().WithUrl("http://localhost:5000/ModelBuilder").WithAutomaticReconnect().Build();
await connection.StartAsync();

connection.On<object>("SendResult", mass => {
    Console.WriteLine(mass);
});

connection.On<object>("UpdateStatus", totalModelsBuilt => {
    Console.WriteLine(totalModelsBuilt);
});

await connection.SendAsync("Build", 10, 20, 30);

Console.ReadLine();
