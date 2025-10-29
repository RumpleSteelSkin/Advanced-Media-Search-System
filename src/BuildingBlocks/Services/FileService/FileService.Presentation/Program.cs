using Core.Configuration.Extensions;
using FileService.Application.Extensions;
using FileService.Persistence.Extensions;
using FileService.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

#region Core

builder.Services.AddCoreConfiguration(builder.Configuration);
builder.Configuration.AddCoreSharedConfiguration();
builder.Services.AddCoreJwtAuthentication(builder.Configuration);

#endregion

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices();
builder.Services.AddPresentationServices();

var app = builder.Build();

app.AddPresentationApp();