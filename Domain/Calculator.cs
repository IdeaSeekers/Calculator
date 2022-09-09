namespace Domain;

public readonly record struct CalculationQuery(string QueryString);

public readonly record struct CalculationResult(double Result);