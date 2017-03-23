using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    public class StringCalculator
    {
        public int Add(string input)
        {
            var parser = new InputParser();

            if (parser.HasSpecifiedDelimiter(input))
                parser.UpdateDelimiter(parser.GetDelimiterFromInput(input));

            return AddNumbersInAString(parser.GetNumberStringFromInput(input), parser);
        }

        private int AddNumbersInAString(string numbers, InputParser parser)
        {
            var sum = 0;

            foreach (var number in numbers.Split(parser.GetDelimiters()))
                sum += parser.GetNumericValueFromString(number);

            return sum;
        }
    }
}
