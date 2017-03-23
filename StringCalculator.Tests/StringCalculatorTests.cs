using NUnit.Framework;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StringCalculator.Tests
{
    [TestFixture]
    public class StringCalculatorTests
    {
        private const string COMMA = ",";
        
        private string _newLineDelimiter;
        private Fixture _fixture;
        private StringCalculator _calculator;

        [SetUp]
        public void SetUp()
        {
            _newLineDelimiter = InputParser.NEW_LINE_DELIMITER.ToString();
            _fixture = new Fixture();
            _calculator = new StringCalculator();
        }
        
        [Test]
        public void Add_GivenEmptyString_Returns0()
        {
            var emptyString = "";
            var expected = 0;

            var actual = _calculator.Add(emptyString);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(NumberCounts))]
        public void Add_GivenUnknownNumberStringWithCommaDelimiter_ReturnsSumOfNumbers(int count)
        {
            var ints = _fixture.CreateMany<int>(count: count);
            var numbers = string.Join(COMMA, ints);
            var expected = ints.Sum();

            var actual = _calculator.Add(numbers);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(NumberCounts))]
        public void Add_GivenUnknownNumberStringWithCommaAndNewLineDelimiters_ReturnsSumOfNumbers(int count)
        {
            var ints = _fixture.CreateMany<int>(count: count).ToArray();
            var numbers = "";
            var expected = ints.Sum();
            for (int i = 0; i < ints.Length; i++)
                numbers += ints[i] + (i % 2 == 0 ? COMMA : _newLineDelimiter);

            var actual = _calculator.Add(numbers);

            
        }

        [TestCaseSource(nameof(NumberCounts))]
        public void Add_GivenUnknownNumberStringWithNewLineDelimiter_ReturnsSumOfNumbers(int count)
        {
            var ints = _fixture.CreateMany<int>(count: count);
            var numbers = string.Join(_newLineDelimiter, ints);
            var expected = ints.Sum();

            var actual = _calculator.Add(numbers);
            
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(NumberCounts))]
        public void Add_GivenUnknownNumberStringWithNewLineAndSpecifiedDelimiters_ReturnsSumOfNumbers(int count)
        {
            var ints = _fixture.CreateMany<int>(count: count).ToArray();
            var delimiter = _fixture.CreateNonDigitChar();
            var numbers = TestDataGenerator.GetDelimiterString(delimiter);
            var expected = ints.Sum();
            for (int i = 0; i < ints.Length; i++)
                numbers += ints[i] + (i % 2 == 0 ? delimiter.ToString() : _newLineDelimiter);

            var actual = _calculator.Add(numbers);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(NumberCounts))]
        public void Add_GivenUnknownNumberStringWithSpecifiedDelimiters_ReturnsSumOfNumbers(int count)
        {
            var ints = _fixture.CreateMany<int>(count: count);
            var delimiter = _fixture.CreateNonDigitChar();
            var numbers = TestDataGenerator.GetDelimiterString(delimiter)
                + string.Join(delimiter.ToString(), ints);
            var expected = ints.Sum();

            var actual = _calculator.Add(numbers);

            Assert.That(actual, Is.EqualTo(expected));
        }

        private static IEnumerable<int> NumberCounts()
        {
            yield return 1;
            yield return 2;
            yield return 3;
            yield return 5;
            yield return 10;
        }
    }
}
