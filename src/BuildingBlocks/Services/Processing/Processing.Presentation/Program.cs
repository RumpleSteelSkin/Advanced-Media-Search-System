using Core.Configuration.Extensions;
using Processing.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

#region Core

builder.Services.AddCoreConfiguration(builder.Configuration);
builder.Configuration.AddCoreSharedConfiguration();

#endregion

builder.Services.AddPresentationServices(builder.Configuration);

var app = builder.Build();

app.AddPresentationApp();