using Domain;
using org.mariuszgromada.math.mxparser;

namespace Calculator;

public class CalculatorAPI
{
    public static CalculationResult Calculate(CalculationQuery query)
    {
        var expression = new Expression(query.QueryString);
        var result = expression.calculate();
        return CalculationResult.Success(result);
        // return CalculationResult.Failure("???");
    }
}