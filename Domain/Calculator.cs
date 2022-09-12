using FluentResults;

namespace Domain;

public readonly record struct CalculationQuery(string QueryString);

public readonly record struct CalculationResult(Result<double> Result);

public readonly record struct CalculationData(CalculationQuery Query, CalculationResult Result);