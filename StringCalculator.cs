using System;
using System.Collections.Generic;
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
            string[] delims = new string[] { ",", "\n" };
            if (numbers.StartsWith("//") && numbers.Contains('\n'))
            {
                int end = numbers.IndexOf('\n') + 1;
                delims = GetDelimiters(numbers.Substring(0, end));
                numbers = numbers.Substring(end);
            }

            return numbers.Split(delims, StringSplitOptions.RemoveEmptyEntries).Select(ConvertToNumber).Sum();
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
            else if (integerValue > 1000)
            {
                return 0;
            }

            return integerValue;
        }

        public static string[] GetDelimiters(string delimiterString)
        {
            List<string> delims = new List<string>(new string[] { ",", "\n" });

            if (delimiterString.StartsWith("//") && delimiterString.EndsWith("\n"))
            {
                delims = new List<string>(new string[] { string.Empty });
                delimiterString = delimiterString.Trim(new char[] { '/', '\n' });
                bool multiChar = false;
                foreach (char x in delimiterString)
                {
                    delims[delims.Count() - 1] += x;
                    delims[delims.Count() - 1] = delims[delims.Count() - 1].Trim('[', ']');
                    if (!multiChar)
                    {
                        delims.Add(string.Empty);
                        multiChar = x == '[';
                    }
                    else
                    {
                        multiChar = x != ']';
                    }
                }
            }

            delims.Remove(string.Empty);

            return delims.ToArray();
        }
    }
}
