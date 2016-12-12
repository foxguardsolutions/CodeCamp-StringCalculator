using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class InputParser
    {
        private static string[] defaultDelimiters = { ",", "\n" };

        public IEnumerable<int> ParseToInts(string input)
        {
            var splitNumbers = SplitInupt(input);
            return ConvertMembersToInts(splitNumbers);
        }

        private string[] SplitInupt(string input)
        {
            var delimiters = GetDelimiters(input);
            var numbers = GetNumbers(input);
            return numbers.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        }

        private IEnumerable<int> ConvertMembersToInts(string[] splitNumbers)
        {
            return from number in splitNumbers
                   select int.Parse(number);
        }

        private string[] GetDelimiters(string input)
        {
            if (StringSpecifiesDelimiters(input))
            {
                return ExtractDelimiterFromSpecification(input);
            }

            return defaultDelimiters;
        }

        private string GetNumbers(string input)
        {
            if (StringSpecifiesDelimiters(input))
            {
                var numberStringStart = GetStartPositionOfNumbers(input);
                return input.Substring(numberStringStart);
            }

            return input;
        }

        private bool StringSpecifiesDelimiters(string input)
        {
            return input.StartsWith("//");
        }

        private int GetStartPositionOfNumbers(string input)
        {
            if (input.Contains("]\n"))
            {
                return input.IndexOf("]\n") + 2;
            }

            return 4;
        }

        private string[] ExtractDelimiterFromSpecification(string input)
        {
            if (StringSpecifiesMulticharacterDelimiter(input))
            {
                return new string[] { GetMulticharacterDelimiter(input) };
            }

            return new string[] { input[2].ToString() };
        }

        private bool StringSpecifiesMulticharacterDelimiter(string input)
        {
            return input.StartsWith("//[");
        }

        private string GetMulticharacterDelimiter(string input)
        {
            var start = 3;
            var length = input.IndexOf("]") - start;
            return input.Substring(start, length);
        }
    }
}
