using Calculator;
using Domain;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Logging.AddConsole();

// TODO: add Database service

var app = builder.Build();

app.MapControllers();

app.Logger.LogInformation("LAUNCHING");
app.Run();