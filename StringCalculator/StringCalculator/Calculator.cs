using System.Linq;

namespace StringCalculator
{
    public class Calculator
    {
        public int Add(string input)
        {
            var parser = new InputParser();
            var numberValidator = new Validator();
            var addends = parser.ParseToInts(input);
            numberValidator.ValidateNumbers(addends);
            return addends.Sum();
        }
    }
}
