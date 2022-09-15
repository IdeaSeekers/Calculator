using Auth;
using Database;

var builder = WebApplication.CreateBuilder(args);

var databaseUser = Environment.GetEnvironmentVariable("DATABASE_USER")
                   ?? throw new Exception("No DATABASE_USER found");
var databasePassword = Environment.GetEnvironmentVariable("DATABASE_PASSWORD")
                       ?? throw new Exception("No DATABASE_PASSWORD found");
var databaseName = Environment.GetEnvironmentVariable("DATABASE_NAME")
                   ?? throw new Exception("No DATABASE_NAME found");

builder.Services.AddSingleton(new DatabaseAPI(databaseUser, databasePassword, databaseName));
builder.Services.AddSingleton<IAuthApi, AuthApiImpl>();
builder.Logging.AddConsole();

builder.Services.AddControllers();

// TODO: add Database service

var app = builder.Build();

app.MapControllers();

app.Logger.LogInformation("LAUNCHING");
app.Run();