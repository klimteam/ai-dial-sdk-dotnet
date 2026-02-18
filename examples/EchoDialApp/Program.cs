using EchoDialApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDialApplication<EchoDialApplication>();

var app = builder.Build();
app.AppendDialAppMiddlewares();

app.Run();