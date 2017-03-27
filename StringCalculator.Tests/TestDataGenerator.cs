using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Tests
{
    public static class TestDataGenerator
    {
        public const string COMMA = ",";
        
        public static string GetDelimiterString(string delimiter)
        {
            return InputParser.DELIMITER_DEFINER + InputParser.DELIMITER_OPENER + delimiter
                + InputParser.DELIMITER_CLOSER + InputParser.NEW_LINE_DELIMITER;
        }

        public static string GetSingleCharDelimiterString(char delimiter)
        {
            return InputParser.DELIMITER_DEFINER + delimiter + InputParser.NEW_LINE_DELIMITER;
        }
    }
}
