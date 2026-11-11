using AiDialSdk.Applications;
using FormGeneratorDialApp;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddDialApplication<FormGeneratorDialApplication>()
    .AddApplicationDialApiClient();

var app = builder.Build();
app.AppendDialAppMiddlewares();

app.Run();