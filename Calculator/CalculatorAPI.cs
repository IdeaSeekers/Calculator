using Domain;
using FluentResults;
using org.mariuszgromada.math.mxparser;

namespace Calculator;

public class CalculatorAPI
{
    public static CalculationResult Calculate(CalculationQuery query)
    {
        try
        {
            var userInput = ReplaceLog10(query.QueryString);
            var expression = new Expression(userInput);
            var result = expression.calculate();
            return new CalculationResult(Result.Ok(result));
        }
        catch (Exception e)
        {
            return new CalculationResult(Result.Fail(e.Message));
        }
    }

    private static string ReplaceLog10(string userInput)
    {
        return userInput.Replace("log", "log10");
    }
}