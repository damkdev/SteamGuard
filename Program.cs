IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {        
        services.AddHostedService<SteamGuard.Worker.Service>();
    })
    .Build();

host.Run();
