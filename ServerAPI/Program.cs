using Auth;
using Database;

var builder = WebApplication.CreateBuilder(args);

var databaseUser = "aaa";
var databasePassword = "bb";
var databaseName = "cc";

builder.Services.AddSingleton(new DatabaseAPI(databaseUser, databasePassword, databaseName));
builder.Services.AddSingleton<IAuthApi, AuthApiImpl>();
builder.Logging.AddConsole();

builder.Services.AddControllers();

// TODO: add Database service

var app = builder.Build();

app.MapControllers();

app.Logger.LogInformation("LAUNCHING");
app.Run();