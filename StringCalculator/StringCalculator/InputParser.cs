using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class InputParser
    {
        private static string[] defaultDelimiters = { ",", "\n" };
        private static string customDelimiterIndicator = "//";
        private static string complexCustomDelimiterIndicator = "//[";
        private static string complexCustomDelimiterTerminator = "]\n";
        private static string complexCustomDelimiterSeparator = "][";

        public IEnumerable<int> ParseToInts(string input)
        {
            var splitNumbers = SplitInput(input);
            return ConvertMembersToInts(splitNumbers);
        }

        private string[] SplitInput(string input)
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
                return ExtractDelimitersFromSpecification(input);
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
            return input.StartsWith(customDelimiterIndicator);
        }

        private int GetStartPositionOfNumbers(string input)
        {
            if (input.Contains(complexCustomDelimiterTerminator))
            {
                return input.IndexOf(complexCustomDelimiterTerminator) + complexCustomDelimiterTerminator.Length;
            }

            var simpleCustomDelimiterAndTerminatorLength = 2;
            return customDelimiterIndicator.Length + simpleCustomDelimiterAndTerminatorLength;
        }

        private string[] ExtractDelimitersFromSpecification(string input)
        {
            if (StringSpecifiesComplexDelimiters(input))
            {
                return GetComplexDelimiters(input);
            }

            return new string[] { input[customDelimiterIndicator.Length].ToString() };
        }

        private bool StringSpecifiesComplexDelimiters(string input)
        {
            return input.StartsWith(complexCustomDelimiterIndicator);
        }

        private string[] GetComplexDelimiters(string input)
        {
            var start = complexCustomDelimiterIndicator.Length;
            var length = input.IndexOf(complexCustomDelimiterTerminator) - start;
            var complexDelimiterText = input.Substring(start, length);
            return complexDelimiterText.Split(new string[] { complexCustomDelimiterSeparator }, StringSplitOptions.RemoveEmptyEntries).ToArray();
        }
    }
}
