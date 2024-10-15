using AutomatedFileSortService;

var hoster = Host.CreateDefaultBuilder(args)
    .UseWindowsService(conf => conf.ServiceName = "File Sort Service");
var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
