using Identity.Application.Extensions;
using Identity.Persistence.Extensions;
using Identity.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentationServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();


var app = builder.Build();


app.AddPresentationApp();