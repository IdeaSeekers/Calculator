using Auth;
using Calculator;
using Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<CalculatorAPI>();
builder.Services.AddSingleton<DatabaseAPI>();
builder.Services.AddSingleton<AuthAPI>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/calculate", () => 
    $"Calculate {app.Services.GetService<CalculatorAPI>()?.GetType().Name}"
);
app.MapPost("/register", () => 
    $"Register {app.Services.GetService<AuthAPI>()?.GetType().Name}"
);
app.MapPost("/signin", () => 
    $"Sign in {app.Services.GetService<AuthAPI>()?.GetType().Name}"
);
app.MapGet("/history", () =>
    $"History {app.Services.GetService<DatabaseAPI>()?.GetType().Name}"
);

app.Run();