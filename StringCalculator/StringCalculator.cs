using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    public class StringCalculator
    {
        internal const int CALCULATOR_INPUT_UPPER_LIMIT = 1000;
        internal const string NEGATIVE_NUMBER_MSG = "Negatives not allowed ";

        private const string COMMA = ",";
        private const string NEGATIVE_NUMBER_START = "-";
        
        public int Add(string input)
        {
            var parser = new InputParser();

            if (parser.HasSpecifiedDelimiter(input))
                parser.UpdateDelimiter(parser.GetDelimiterFromInput(input));

            return AddNumbersInAString(parser.GetNumberStringFromInput(input), parser);
        }

        private int AddNumbersInAString(string numberString, InputParser parser)
        {
            var numbers = numberString.Split(parser.GetDelimiters());
            
            VerifyNoNegativeNumbers(numbers);

            var sum = numbers.Select(n => parser.GetNumericValueFromString(n))
                .Where(n => !IsIgnoredValue(n))
                .Sum();

            return sum;
        }

        private bool IsIgnoredValue(int number)
        {
            return number > CALCULATOR_INPUT_UPPER_LIMIT;
        }

        private void VerifyNoNegativeNumbers(string[] numbers)
        {
            var negativeNumbers = numbers.Where(s => s.StartsWith(NEGATIVE_NUMBER_START));

            if (negativeNumbers.Any())
                throw new NegativeNumberException(NEGATIVE_NUMBER_MSG
                    + string.Join(COMMA, negativeNumbers));
        }
    }
}
