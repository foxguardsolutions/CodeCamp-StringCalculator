using System.Linq;

namespace StringCalculator
{
    public class Calculator
    {
        private InputParser _parser = new InputParser();
        private Validator _numberValidator = new Validator();

        public int Add(string input)
        {
            var addends = _parser.ParseToInts(input);
            var validAddends = _numberValidator.Validate(addends);
            return validAddends.Sum();
        }
    }
}
