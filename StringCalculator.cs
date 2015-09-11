using System;
using System.Linq;

namespace StringCalculator
{
    public class StringCalculator
    {
        public static void Main(string[] args)
        {
        }

        public static int Add(string numbers)
        {
            char[] delims = new char[] { ',', '\n' };
            if (numbers.StartsWith("//") && numbers.Contains('\n'))
            {
                int end = numbers.IndexOf('\n') - 2;
                delims = numbers.Substring(2, end).ToCharArray();
                numbers = numbers.Substring(numbers.IndexOf('\n') + 1);
            }

            return numbers.Split(delims).Select(ConvertToNumber).Sum();
        }

        public static int ConvertToNumber(string number)
        {
            if (number == string.Empty)
            {
                return 0;
            }

            int integerValue = Convert.ToInt32(number, 10);
            if (integerValue < 0)
            {
                throw new ArgumentException("Negatives are not allowed", "number");
            }

            return integerValue;
        }
    }
}
