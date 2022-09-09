namespace Domain;

public readonly record struct CalculationHistory(CalculationResult[] Data);

// TODO: make it optional with fail reason
public readonly record struct UpdateHistoryResult(CalculationHistory History);

public readonly record struct GetHistoryResult(CalculationHistory History);

public readonly record struct RegisterUserDatabaseResult(bool Successful);

public readonly record struct GetUserResult(User User);
