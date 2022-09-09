namespace Domain;

public readonly record struct Token(string Data);

// TODO : make optional with fail reasons
public readonly record struct RegisterResult(bool Success);

public readonly record struct SignInResult(Token Token);

public readonly record struct VerifyResult(UserInfo Token);
