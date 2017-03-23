using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Tests
{
    public static class TestDataGenerator
    {
        public static string GetDelimiterString(char delimiter)
        {
            return InputParser.DELIMITER_DEFINER + delimiter + InputParser.NEW_LINE_DELIMITER;
        }
    }
}
