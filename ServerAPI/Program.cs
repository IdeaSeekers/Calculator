var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/calculate", () => "Calculate");
app.MapGet("/register", () => "Register");
app.MapGet("/signin", () => "Sign in");
app.MapGet("/history", () => "History");

app.Run();