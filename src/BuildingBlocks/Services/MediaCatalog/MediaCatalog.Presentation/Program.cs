using Core.Configuration.Extensions;
using MediaCatalog.Application.Extensions;
using MediaCatalog.Persistence.Extensions;
using MediaCatalog.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

#region Core

builder.Services.AddCoreConfiguration(builder.Configuration);
builder.Configuration.AddCoreSharedConfiguration();
builder.Services.AddCoreJwtAuthentication(builder.Configuration);

#endregion

builder.Services.AddApplicationServices();
builder.Services.AddPresentationServices(builder.Configuration);
builder.Services.AddPersistenceServices();

var app = builder.Build();
app.AddPresentationApp();