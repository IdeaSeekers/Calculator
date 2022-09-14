using Calculator;
using Domain;

namespace UnitTest
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        [TestCase("0", 0)]
        [TestCase("1", 1)]
        [TestCase("+1", 1)]
        [TestCase("-1", -1)]
        [TestCase("(7)", 7)]
        [TestCase("((9))", 9)]
        public void TrivialTests(string userInput, double expected)
        {
            var actual = CalculatorAPI.Calculate(new CalculationQuery(userInput)).Result.Value;
            Assert.That(expected, Is.EqualTo(actual));
        }
        
        [Test]
        [TestCase("1 + 2", 3)]
        [TestCase("1 - 2", -1)]
        [TestCase("2 * 3", 6)]
        [TestCase("4 / 2", 2)]
        [TestCase("15 / 4", 3.75)]
        [TestCase("3^4", 81)]
        [TestCase("6!", 720)]
        [TestCase("log(10)", 1)]
        [TestCase("ln(e * e)", 2)]
        [TestCase("sqrt(9)", 3)]
        public void SimpleTests(string userInput, double expected)
        {
            var actual = CalculatorAPI.Calculate(new CalculationQuery(userInput)).Result.Value;
            Assert.That(expected, Is.EqualTo(actual));
        }
        
        [Test]
        [TestCase("2 + 2 * 2", 6)]
        [TestCase("(2 + 2) * 2", 8)]
        [TestCase("12 - 1 + log(10)^2 - log(10^2) * 2 + sqrt(9) / 2! + 11", 20.5)]
        [TestCase("12 - (1 + (log(10)^2 - log(10^2)) * 2 + sqrt(9)) / 2! + 11", 22)]
        [TestCase("(12 - 1 + log(10)^2 - log(10^2)) * (2 + sqrt(9)) / 2! + 11", 36)]
        public void ParenthesisTests(string userInput, double expected)
        {
            var actual = CalculatorAPI.Calculate(new CalculationQuery(userInput)).Result.Value;
            Assert.That(expected, Is.EqualTo(actual));
        }
        
        [Test]
        [TestCase("(3!)!", 720)]
        [TestCase("(2^3)^2", 64)]
        [TestCase("2^3^2", 512)]
        [TestCase("log(log(10^10))", 1)]
        [TestCase("sqrt(sqrt(16))", 2)]
        [TestCase("sqrt(log(10^9))", 3)]
        [TestCase("ln(sqrt(e^2))", 1)]
        [TestCase("sqrt(ln(log(10^e)))", 1)]
        [TestCase("ln(e^log(10! / 36288))", 2)]
        public void NestedOperationsTests(string userInput, double expected)
        {
            var actual = CalculatorAPI.Calculate(new CalculationQuery(userInput)).Result.Value;
            Assert.That(expected, Is.EqualTo(actual));
        }
        
        [Test]
        [TestCase("(1 + 2")]
        [TestCase("(1 + 2))")]
        [TestCase("sqr(1 + 2)")]
        [TestCase("sqrt 1 + 2)")]
        [TestCase("sqrt(1 2)")]
        [TestCase("sqrt(1")]
        public void SyntaxErrorsTests(string userInput)
        {
            var actual = CalculatorAPI.Calculate(new CalculationQuery(userInput));
            Assert.That(actual.Result.IsFailed, Is.True);
        }
        
        [Test]
        [TestCase("1 / 0")]
        [TestCase("ln(-5)")]
        [TestCase("log(-7)")]
        [TestCase("sqrt(-9)")]
        public void NanTests(string userInput)
        {
            var actual = CalculatorAPI.Calculate(new CalculationQuery(userInput));
            Assert.That(actual.Result.Reasons.First().Message, Is.EqualTo("Not a number"));
        }
        
        [Test]
        [TestCase("(10!)!")]
        [TestCase("10^10^10")]
        public void InfinityTests(string userInput)
        {
            var actual = CalculatorAPI.Calculate(new CalculationQuery(userInput));
            Assert.That(actual.Result.Reasons.First().Message, Is.EqualTo("Infinity"));
        }
    }
}
