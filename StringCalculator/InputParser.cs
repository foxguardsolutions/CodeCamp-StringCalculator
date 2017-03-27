using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    public class InputParser
    {
        internal const string DELIMITER_CLOSER = "]";
        internal const string DELIMITER_DEFINER = "//";
        internal const string DELIMITER_OPENER = "[";
        internal const string NEW_LINE_DELIMITER = "\n";
        
        private string _defaultDelimiter = ",";

        public string GetDelimiterFromInput(string input)
        {
            var delimiterIntroLength = DELIMITER_DEFINER.Length + DELIMITER_OPENER.Length;
            var delimiter = HasSpecifiedMultiCharDelimiter(input) ?
                input.Substring(delimiterIntroLength, input.IndexOf(DELIMITER_CLOSER) - delimiterIntroLength) :
                input.Substring(DELIMITER_DEFINER.Length, 1);
            
            return delimiter;
        }

        public string[] GetDelimiters()
        {
            return new string[] { _defaultDelimiter, NEW_LINE_DELIMITER };
        }

        public string GetNumberStringFromInput(string input)
        {
            return input.Substring(GetDelimiterDefinitionLength(input));
        }

        public int GetNumericValueFromString(string number)
        {
            return string.IsNullOrWhiteSpace(number) ? 0 : int.Parse(number);
        }

        public bool HasSpecifiedDelimiter(string input)
        {
            return input.StartsWith(DELIMITER_DEFINER);
        }

        private int GetDelimiterDefinitionLength(string input)
        {
            var delimiterExitLength = DELIMITER_CLOSER.Length + NEW_LINE_DELIMITER.Length;
            return HasSpecifiedMultiCharDelimiter(input) ? input.IndexOf(DELIMITER_CLOSER + NEW_LINE_DELIMITER) + delimiterExitLength :
                   HasSpecifiedDelimiter(input) ? input.IndexOf(NEW_LINE_DELIMITER) + NEW_LINE_DELIMITER.Length : 0;
        }

        private bool HasSpecifiedMultiCharDelimiter(string input)
        {
            return HasSpecifiedDelimiter(input)
                && input.Substring(DELIMITER_DEFINER.Length, DELIMITER_OPENER.Length) == DELIMITER_OPENER
                && input.Contains(DELIMITER_CLOSER);
        }

        public void UpdateDelimiter(string delimiter)
        {
            _defaultDelimiter = delimiter;
        }
    }
}
