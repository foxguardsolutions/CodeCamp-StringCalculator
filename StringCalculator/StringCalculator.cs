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
        private const char NEW_LINE = '\n';

        public int Add(string numbers)
        {
            var sum = 0;
            
            foreach (var number in numbers.Split(COMMA, NEW_LINE))
                sum += GetNumericValueFromString(number);
            
            return sum;
        }

        private int GetNumericValueFromString(string number)
        {
            return string.IsNullOrWhiteSpace(number) ? 0 : int.Parse(number);
        }
    }
}
