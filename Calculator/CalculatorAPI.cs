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

    public static void main()
    {
        var result = Calculate(new CalculationQuery("1 + 2"));
        Console.WriteLine(result);
    }
}