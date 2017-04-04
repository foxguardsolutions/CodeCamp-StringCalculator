using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Tests
{
    public static class TestDataGenerator
    {
        public static string GetDelimiterString(string delimiter)
        {
            return InputParser.DELIMITER_DEFINER + InputParser.DELIMITER_OPENER + delimiter
                + InputParser.DELIMITER_CLOSER + InputParser.NEW_LINE_DELIMITER;
        }

        public static string GetDelimiterString(IEnumerable<char> delimiters)
        {
            return GetDelimiterString(delimiters.Select(c => c.ToString()));
        }

        public static string GetDelimiterString(IEnumerable<string> delimiters)
        {
            var delimiterString = InputParser.DELIMITER_DEFINER;

            foreach (var delimiter in delimiters)
                delimiterString += InputParser.DELIMITER_OPENER + delimiter.ToString() + InputParser.DELIMITER_CLOSER;

            return delimiterString + InputParser.NEW_LINE_DELIMITER;
        }

        public static string GetNumberString(IEnumerable<int> ints, IEnumerable<char> delimiters)
        {
            return GetNumberString(ints, delimiters.Select(c => c.ToString()));
        }

        public static string GetNumberString(IEnumerable<int> ints, IEnumerable<string> delimiters)
        {
            var delimList = delimiters.ToList();
            var i = 0;
            var numberString = "";

            foreach (var number in ints)
            {
                if (i == delimList.Count) i = 0;
                numberString += number + delimList[i].ToString();
                i++;
            }

            return numberString;
        }

        public static string GetSingleCharDelimiterString(char delimiter)
        {
            return InputParser.DELIMITER_DEFINER + delimiter + InputParser.NEW_LINE_DELIMITER;
        }
    }
}
