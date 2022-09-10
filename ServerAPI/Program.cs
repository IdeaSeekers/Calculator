var builder = WebApplication.CreateBuilder(args);

// TODO: add Database service

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/calculate", () => "Calculate");
app.MapPost("/register", () => "Register");
app.MapPost("/signin", () => "Sign in");
app.MapGet("/history", () => "History");

app.Run();