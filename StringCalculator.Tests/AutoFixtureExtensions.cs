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

        public static IEnumerable<int> CreateManyIntsInRange(this Fixture fixture, int minValue, int maxValue, int count = 3)
        {
            return fixture.Build<int>().FromFactory(() => fixture.CreateIntInRange(minValue, maxValue)).CreateMany(count);
        }

        public static IEnumerable<int> CreateManyNegativeInts(this Fixture fixture, int count = 3)
        {
            return fixture.Build<int>().FromFactory(() => fixture.CreateNegativeInt()).CreateMany(count);
        }

        public static int CreateIntInRange(this Fixture fixture, int minValue, int maxValue)
        {
            if (minValue >= maxValue)
                throw new ArgumentException("The minimum value must be less than the maximum value");

            return (fixture.Create<int>() % (maxValue - minValue)) + minValue;
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
    }
}
