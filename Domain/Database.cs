using FluentResults;

namespace Domain;

public readonly record struct CalculationHistory(CalculationData[] Data);

public readonly record struct UpdateHistoryResult(Result<CalculationHistory> History);

public readonly record struct GetHistoryResult(Result<CalculationHistory> History);

public readonly record struct RegisterUserDatabaseResult(Result Result);

public readonly record struct GetUserResult(Result<User> User);
