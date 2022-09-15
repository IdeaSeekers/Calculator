namespace Domain;

public record class Password(string Data);

public record class Login(string Data);

public record class UserInfo(Login Login);

public record class User(Login Login, Password Password);
