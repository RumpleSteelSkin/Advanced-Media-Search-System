using FileService.Application.Extensions;
using FileService.Persistence.Extensions;
using FileService.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices();
builder.Services.AddPresentationServices(builder.Configuration);

var app = builder.Build();

app.AddPresentationApp();