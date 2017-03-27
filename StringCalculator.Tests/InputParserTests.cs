using NUnit.Framework;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Tests
{
    [TestFixture]
    public class InputParserTests
    {
        private string _comma;
        private string _newLineDelimiter;
        private string[] _illegalDelimiters;
        private Fixture _fixture;
        private InputParser _parser;

        [SetUp]
        public void SetUp()
        {
            _comma = TestDataGenerator.COMMA;
            _newLineDelimiter = InputParser.NEW_LINE_DELIMITER.ToString();
            _illegalDelimiters = new[] { InputParser.DELIMITER_OPENER, InputParser.DELIMITER_CLOSER };
            _fixture = new Fixture();
            _parser = new InputParser();
        }

        [Test]
        public void GetDelimiterFromInput_GivenDelimiterString_ReturnsDelimiter()
        {
            var delimiter = _fixture.CreateNonDigitChar();
            var delimiterString = TestDataGenerator.GetSingleCharDelimiterString(delimiter);
            var expected = delimiter.ToString();

            var actual = _parser.GetDelimiterFromInput(delimiterString);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetDelimiterFromInput_GivenMultiCharDelimiter_ReturnsDelimiter()
        {
            var expected = _fixture.CreateStringExcludingCharSequences(_illegalDelimiters);
            var delimiterString = TestDataGenerator.GetDelimiterString(expected);

            var actual = _parser.GetDelimiterFromInput(delimiterString);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetDelmiters_GivenUnchangedParser_ReturnsDefaultDelimiters()
        {
            var expected = new[] { _comma, _newLineDelimiter };

            var actual = _parser.GetDelimiters();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetNumberStringFromInput_GivenStringOfNumbers_ReturnsSameString()
        {
            var ints = _fixture.CreateMany<int>();
            var expected = string.Join(_comma, ints);

            var actual = _parser.GetNumberStringFromInput(expected);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetNumberStringFromInput_GivenStringOfNumbersWithSpecifiedDelimiter_ReturnsStringOfNumbers()
        {
            var ints = _fixture.CreateMany<int>();
            var delimiter = _fixture.CreateNonDigitChar();
            var delimiterString = TestDataGenerator.GetSingleCharDelimiterString(delimiter);
            var expected = string.Join(delimiter.ToString(), ints);
            var input = delimiterString + expected;

            var actual = _parser.GetNumberStringFromInput(input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetNumberStringFromInput_GivenStringOfNumbersWithSpecifiedMultiCharDelimiter_ReturnsStringOfNumbers()
        {
            var ints = _fixture.CreateMany<int>();
            var delimiter = _fixture.CreateStringExcludingCharSequences(_illegalDelimiters);
            var delimiterString = TestDataGenerator.GetDelimiterString(delimiter);
            var expected = string.Join(delimiter, ints);
            var input = delimiterString + expected;

            var actual = _parser.GetNumberStringFromInput(input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetNumericValueFromString_GivenEmptyString_Returns0()
        {
            var emptyString = "";
            var expected = 0;

            var actual = _parser.GetNumericValueFromString(emptyString);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetNumericValueFromString_GivenNumberString_ReturnsNumberValue()
        {
            var expected = _fixture.Create<int>();

            var actual = _parser.GetNumericValueFromString(expected.ToString());

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void HasSpecifiedDelimiter_GivenDelimiterString_ReturnsTrue()
        {
            var delimiter = _fixture.CreateNonDigitChar();
            var input = TestDataGenerator.GetSingleCharDelimiterString(delimiter);

            var actual = _parser.HasSpecifiedDelimiter(input);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void HasSpecifiedDelimiter_GivenEmptyString_ReturnsFalse()
        {
            var emptyString = "";

            var actual = _parser.HasSpecifiedDelimiter(emptyString);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void HasSpecifiedDelimiter_GivenMultiCharDelimiterString_ReturnsTrue()
        {
            var delimiter = _fixture.CreateStringExcludingCharSequences(_illegalDelimiters);
            var input = TestDataGenerator.GetDelimiterString(delimiter);

            var actual = _parser.HasSpecifiedDelimiter(input);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void HasSpecifiedDelimiter_GivenNumberString_ReturnsFalse()
        {
            var ints = _fixture.CreateMany<int>();
            var input = string.Join(_comma, ints);

            var actual = _parser.HasSpecifiedDelimiter(input);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void HasSpecifiedDelimiter_GivenNumberStringWithSpecifiedDelimiter_ReturnsTrue()
        {
            var ints = _fixture.CreateMany<int>();
            var delimiter = _fixture.CreateNonDigitChar();
            var input = TestDataGenerator.GetSingleCharDelimiterString(delimiter)
                + string.Join(delimiter.ToString(), ints);

            var actual = _parser.HasSpecifiedDelimiter(input);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void HasSpecifiedDelimiter_GivenNumberStringWithSpecifiedMultiCharDelimiter_ReturnsTrue()
        {
            var delimiter = _fixture.CreateStringExcludingCharSequences(_illegalDelimiters);
            var input = TestDataGenerator.GetDelimiterString(delimiter)
                + string.Join(delimiter.ToString(), _fixture.CreateMany<int>());

            var actual = _parser.HasSpecifiedDelimiter(input);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void UpdateDelimiter_GivenDelimiter_ChangesDelimiters()
        {
            var delimiter = _fixture.CreateNonDigitChar().ToString();
            var parser = new InputParser();
            var expected = new[] { delimiter, _newLineDelimiter };

            parser.UpdateDelimiter(delimiter);
            var actual = parser.GetDelimiters();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void UpdateDelimiter_GivenMultiCharDelimiter_ChangesDelimiters()
        {
            var delimiter = _fixture.CreateStringExcludingCharSequences(_illegalDelimiters);
            var parser = new InputParser();
            var expected = new[] { delimiter, _newLineDelimiter };

            parser.UpdateDelimiter(delimiter);
            var actual = parser.GetDelimiters();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
