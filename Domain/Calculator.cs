namespace Domain;

public readonly record struct CalculationQuery(string QueryString);

// TODO: make result optional with fail reason
public readonly record struct CalculationResult(double Result);