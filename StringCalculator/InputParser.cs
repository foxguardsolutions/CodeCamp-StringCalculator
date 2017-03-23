using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    public class InputParser
    {
        internal const char NEW_LINE_DELIMITER = '\n';
        internal const string DELIMITER_DEFINER = "//";

        private char _defaultDelimiter = ',';

        public char GetDelimiterFromInput(string input)
        {
            return input[2];
        }

        public char[] GetDelimiters()
        {
            return new char[] { _defaultDelimiter, NEW_LINE_DELIMITER };
        }

        public string GetNumberStringFromInput(string input)
        {
            return HasSpecifiedDelimiter(input) ? input.Substring(4) : input;
        }

        public int GetNumericValueFromString(string number)
        {
            return string.IsNullOrWhiteSpace(number) ? 0 : int.Parse(number);
        }

        public bool HasSpecifiedDelimiter(string input)
        {
            return input.StartsWith(DELIMITER_DEFINER);
        }

        public void UpdateDelimiter(char delimiter)
        {
            _defaultDelimiter = delimiter;
        }
    }
}
