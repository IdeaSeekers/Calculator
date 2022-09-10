using FluentResults;

namespace Domain;

public readonly record struct CalculationQuery(string QueryString);

public readonly record struct CalculationResult(Result<double> Result);