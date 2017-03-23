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
        private const string COMMA = ",";
        
        private string _newLineDelimiter;
        private Fixture _fixture;
        private InputParser _parser;

        [SetUp]
        public void SetUp()
        {
            _newLineDelimiter = InputParser.NEW_LINE_DELIMITER.ToString();
            _fixture = new Fixture();
            _parser = new InputParser();
        }

        [Test]
        public void GetDelimiterFromInput_GivenDelimiterString_ReturnsDelimiter()
        {
            var expected = _fixture.CreateNonDigitChar();
            var delimiterString = TestDataGenerator.GetDelimiterString(expected);

            var actual = _parser.GetDelimiterFromInput(delimiterString);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetDelmiters_GivenUnchangedParser_ReturnsDefaultDelimiters()
        {
            var expected = new[] { COMMA[0], _newLineDelimiter[0] };

            var actual = _parser.GetDelimiters();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetNumberStringFromInput_GivenStringOfNumbers_ReturnsSameString()
        {
            var ints = _fixture.CreateMany<int>();
            var expected = string.Join(COMMA, ints);

            var actual = _parser.GetNumberStringFromInput(expected);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetNumberStringFromInput_GivenStringOfNumbersWithSpecifiedDelimiter_ReturnsStringOfNumbers()
        {
            var ints = _fixture.CreateMany<int>();
            var delimiter = _fixture.CreateNonDigitChar();
            var delimiterString = TestDataGenerator.GetDelimiterString(delimiter);
            var expected = string.Join(delimiter.ToString(), ints);
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
            var input = TestDataGenerator.GetDelimiterString(delimiter);

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
        public void HasSpecifiedDelimiter_GivenNumberString_ReturnsFalse()
        {
            var ints = _fixture.CreateMany<int>();
            var input = string.Join(COMMA, ints);

            var actual = _parser.HasSpecifiedDelimiter(input);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void HasSpecifiedDelimiter_GivenNumberStringWithSpecifiedDelimiter_ReturnsTrue()
        {
            var ints = _fixture.CreateMany<int>();
            var delimiter = _fixture.Create<char>();
            var input = TestDataGenerator.GetDelimiterString(delimiter)
                + string.Join(delimiter.ToString(), ints);

            var actual = _parser.HasSpecifiedDelimiter(input);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void UpdateDelimiter_GivenDelimiter_ChangesDelimiters()
        {
            var delimiter = _fixture.Create<char>();
            var parser = new InputParser();
            var expected = new[] { delimiter, _newLineDelimiter[0] };

            parser.UpdateDelimiter(delimiter);
            var actual = parser.GetDelimiters();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
