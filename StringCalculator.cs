using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
            Regex matcher = new Regex("^//(\\[.*?\\]|.)*\n(.*)");

            if (matcher.IsMatch(numbers))
            {
                delims = GetDelimiters(numbers, matcher);
                numbers = GetAdditionString(numbers, matcher);
            }

            return GetAllNumbers(numbers, delims).Sum();
        }

        public static List<int> GetAllNumbers(string numberString, string[] delimiters)
        {
            List<int> numbersList =
                numberString
                .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                .Select(ConvertToNumber)
                .ToList();
            if (numbersList.Any(x => x < 0))
            {
                int[] negatives = numbersList.Where(x => x < 0).ToArray();
                string format = "I don't know how to add negatives: ";
                foreach (int negative in negatives)
                {
                    format += string.Format("{0} ", negative);
                }

                throw new ArgumentOutOfRangeException(format);
            }

            return numbersList;
        }

        public static int ConvertToNumber(string number)
        {
            if (number == string.Empty)
            {
                return 0;
            }

            int integerValue = Convert.ToInt32(number, 10);
            if (integerValue > 1000)
            {
                return 0;
            }

            return integerValue;
        }

        public static string GetAdditionString(string fullNumberString, Regex matcher)
        {
            MatchCollection matches = matcher.Matches(fullNumberString);
            return matches[0].Groups[2].Value;
        }

        public static string[] GetDelimiters(string fullNumberString, Regex matcher)
        {
            MatchCollection matches = matcher.Matches(fullNumberString);
            List<string> delimiters = (from object capture in matches[0].Groups[1].Captures select capture.ToString()).ToList();
            return delimiters.Select(x => x.Trim('[', ']')).ToArray();
        }
    }
}
