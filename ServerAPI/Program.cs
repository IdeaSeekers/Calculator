using Calculator;
using Domain;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Logging.AddConsole();

// TODO: add Database service

var app = builder.Build();

app.MapControllerRoute(
    name: "calculate",
    pattern: "/",
    defaults: new { controller = "Calculations", action = "Calculate" });

app.MapControllerRoute(
    name: "register",
    pattern: "/",
    defaults: new { controller = "Authorization", action = "SignUp" });

app.MapControllerRoute(
    name: "signin",
    pattern: "/",
    defaults: new { controller = "Authorization", action = "SignIn" });

app.MapControllerRoute(
    name: "history",
    pattern: "/",
    defaults: new { controller = "History", action = "GetHistory" });


app.Logger.LogInformation("LAUNCHING");
app.Run();