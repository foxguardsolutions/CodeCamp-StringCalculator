using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    public class StringCalculator
    {
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
            var sum = 0;
            var numbers = numberString.Split(parser.GetDelimiters());

            VerifyNoNegativeNumbers(numbers);

            foreach (var number in numbers)
                sum += parser.GetNumericValueFromString(number);

            return sum;
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
