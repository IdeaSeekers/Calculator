using FluentResults;

namespace Domain;

public readonly record struct Token(string Data);

public readonly record struct RegisterResult(Result Result);

public readonly record struct SignInResult(Result<Token> Token);

public readonly record struct VerifyResult(Result<UserInfo> Token);
