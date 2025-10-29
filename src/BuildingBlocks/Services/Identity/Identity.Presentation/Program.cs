using Core.Configuration.Extensions;
using Identity.Application.Extensions;
using Identity.Persistence.Extensions;
using Identity.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

#region Core

builder.Services.AddCoreConfiguration(builder.Configuration);
builder.Configuration.AddCoreSharedConfiguration();
builder.Services.AddCoreJwtAuthentication(builder.Configuration);

#endregion


builder.Services.AddPresentationServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();

var app = builder.Build();

app.AddPresentationApp();