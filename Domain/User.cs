namespace Domain;

public readonly record struct Password(string Data);

public readonly record struct Login(string Data);

public record struct UserInfo(Login Login);

public record struct User(Login Login, Password Password);
