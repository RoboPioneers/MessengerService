using Messenger.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(kestrel =>
{
    kestrel.ConfigureEndpointDefaults(configuration =>
    {
        configuration.Protocols = HttpProtocols.Http2;
    });
});

builder.Services.AddGrpc();

var app = builder.Build();

app.Urls.Add("http://localhost:5000");

app.MapGrpcService<RouterImplementer>();

app.Run();