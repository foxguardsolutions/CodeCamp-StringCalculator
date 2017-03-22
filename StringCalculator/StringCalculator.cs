using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    public class StringCalculator
    {
        private const char COMMA = ',';

        public int Add(string numbers)
        {
            var splitNumbers = numbers.Split(COMMA);
            var sum = GetNumericValueFromString(splitNumbers[0]);

            if (splitNumbers.Length > 1)
                sum += GetNumericValueFromString(splitNumbers[1]);

            return sum;
        }

        private int GetNumericValueFromString(string number)
        {
            return string.IsNullOrWhiteSpace(number) ? 0 : int.Parse(number);
        }
    }
}
