using AiDialSdk.Applications;
using FormDialApp;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDialApplication<FormDialApplication>();

var app = builder.Build();
app.AppendDialAppMiddlewares();

app.Run();