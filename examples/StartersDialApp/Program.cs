using AiDialSdk.Applications;
using Microsoft.AspNetCore.Builder;
using StartersDialApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDialApplication<StartersDialApplication>();

var app = builder.Build();
app.AppendDialAppMiddlewares();

app.Run();