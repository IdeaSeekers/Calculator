using Auth;
using Database;

var builder = WebApplication.CreateBuilder(args);

var databaseUser = Environment.GetEnvironmentVariable("DATABASE_USER") ?? "user";
var databasePassword = Environment.GetEnvironmentVariable("DATABASE_PASSWORD") ?? "password";
var databaseName = Environment.GetEnvironmentVariable("DATABASE_NAME") ?? "name";

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder => policyBuilder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
    );
});
builder.Services.AddSingleton(new DatabaseAPI(databaseUser, databasePassword, databaseName));
builder.Services.AddSingleton<IAuthApi, AuthApiImpl>();
builder.Logging.AddConsole();

builder.Services.AddControllers();

// TODO: add Database service

var app = builder.Build();

app.UseCors();
app.MapControllers();

app.Logger.LogInformation("LAUNCHING");
app.Run();