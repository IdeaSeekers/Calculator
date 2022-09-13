using Calculator;
using Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<CalculatorAPI>();

// TODO: add Database service

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/calculate", () => 
    CalculatorAPI.Calculate(new CalculationQuery("log(10 * 9 + 2^3 + 6 / 3! + sqrt(ln(e))) - 0")).Result.Value
);
app.MapPost("/register", () => "Register");
app.MapPost("/signin", () => "Sign in");
app.MapGet("/history", () => "History");

app.Run();