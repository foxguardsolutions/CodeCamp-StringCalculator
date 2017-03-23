using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StringCalculator.Tests
{
    public static class AutoFixtureExtensions
    {
        private const string DIGIT_REGEX_PATTERN = @"\d";

        public static char CreateNonDigitChar(this Fixture fixture)
        {
            var @char = fixture.Create<char>();

            if (Regex.IsMatch(@char.ToString(), DIGIT_REGEX_PATTERN))
                @char += Convert.ToChar(10);

            return @char;
        }
    }
}
