using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    public class InputParser
    {
        internal const string DEFAULT_DELIMITER = ",";
        internal const string DELIMITER_CLOSER = "]";
        internal const string DELIMITER_DEFINER = "//";
        internal const string DELIMITER_OPENER = "[";
        internal const string NEW_LINE_DELIMITER = "\n";

        private List<string> _delimiters;

        public List<string> GetDelimitersFromInput(string input)
        {
            var delimiters = new List<string>();
            
            if (HasSpecifiedMultiCharOrMultipleDelimiters(input))
            {
                var delimiterString = input.Substring(DELIMITER_DEFINER.Length);
                while (HasMoreDelimiters(delimiterString))
                {
                    delimiters.Add(GetFirstDelimiter(delimiterString));
                    delimiterString = GetNextDelimiterString(delimiterString);
                }
            }
            else
                delimiters.Add(input.Substring(DELIMITER_DEFINER.Length, 1));

            return delimiters;
        }

        public string[] GetDelimiters()
        {
            return (_delimiters == null || _delimiters.Count == 0) ? new string[] { DEFAULT_DELIMITER, NEW_LINE_DELIMITER } :
                _delimiters.Concat(new string[] { NEW_LINE_DELIMITER }).ToArray();
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

        public void UpdateDelimiters(IEnumerable<string> delimiters)
        {
            _delimiters = delimiters.ToList();
        }

        private int GetDelimiterDefinitionLength(string input)
        {
            var delimiterExitLength = DELIMITER_CLOSER.Length + NEW_LINE_DELIMITER.Length;
            return HasSpecifiedMultiCharOrMultipleDelimiters(input) ? input.IndexOf(DELIMITER_CLOSER + NEW_LINE_DELIMITER) + delimiterExitLength :
                   HasSpecifiedDelimiter(input) ? input.IndexOf(NEW_LINE_DELIMITER) + NEW_LINE_DELIMITER.Length : 0;
        }

        private string GetFirstDelimiter(string input)
        {
            return input.Substring(DELIMITER_OPENER.Length, input.IndexOf(DELIMITER_CLOSER) - DELIMITER_OPENER.Length);
        }

        private string GetNextDelimiterString(string input)
        {
            return input.Substring(input.IndexOf(DELIMITER_CLOSER) + DELIMITER_CLOSER.Length);
        }

        private bool HasMoreDelimiters(string input)
        {
            return input.StartsWith(DELIMITER_OPENER) && input.Contains(DELIMITER_CLOSER);
        }

        private bool HasSpecifiedMultiCharOrMultipleDelimiters(string input)
        {
            return HasSpecifiedDelimiter(input)
                && input.Substring(DELIMITER_DEFINER.Length, DELIMITER_OPENER.Length) == DELIMITER_OPENER
                && input.Contains(DELIMITER_CLOSER);
        }
    }
}
