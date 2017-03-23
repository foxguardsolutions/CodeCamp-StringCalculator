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

        public static IEnumerable<int> CreateManyNegativeInts(this Fixture fixture, int count = 3)
        {
            return fixture.Build<int>().FromFactory(() => fixture.CreateNegativeInt()).CreateMany(count);
        }

        public static IEnumerable<int> CreateManyPositiveInts(this Fixture fixture, int count = 3)
        {
            return fixture.Build<int>().FromFactory(() => fixture.CreatePositiveInt()).CreateMany(count);
        }

        public static int CreateNegativeInt(this Fixture fixture)
        {
            return -Math.Abs(fixture.Create<int>());
        }

        public static char CreateNonDigitChar(this Fixture fixture)
        {
            var @char = fixture.Create<char>();

            if (Regex.IsMatch(@char.ToString(), DIGIT_REGEX_PATTERN))
                @char += Convert.ToChar(10);

            return @char;
        }

        public static int CreatePositiveInt(this Fixture fixture)
        {
            return Math.Abs(fixture.Create<int>());
        }
    }
}
