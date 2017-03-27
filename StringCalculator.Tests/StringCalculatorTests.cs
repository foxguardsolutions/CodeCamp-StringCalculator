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
    public class StringCalculatorTests
    {
        private int _maxInputValue;
        private string _defaultDelimiter;
        private string _negativeNumberMessage;
        private string _newLineDelimiter;
        private string[] _illegalDelimiters;
        private Fixture _fixture;
        private StringCalculator _calculator;

        [SetUp]
        public void SetUp()
        {
            _defaultDelimiter = InputParser.DEFAULT_DELIMITER;
            _maxInputValue = StringCalculator.CALCULATOR_INPUT_UPPER_LIMIT;
            _negativeNumberMessage = StringCalculator.NEGATIVE_NUMBER_MSG;
            _newLineDelimiter = InputParser.NEW_LINE_DELIMITER.ToString();
            _illegalDelimiters = new[] { InputParser.DELIMITER_OPENER, InputParser.DELIMITER_CLOSER };
            _fixture = new Fixture();
            _calculator = new StringCalculator();
        }

        [Test]
        public void Add_GivenCalculatorInputUpperLimit_ReturnsUpperLimitValue()
        {
            var expected = _maxInputValue;

            var actual = _calculator.Add(_maxInputValue.ToString());

            Assert.That(actual, Is.EqualTo(expected));
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
        public void Add_GivenUnknownNegativeAndPositiveNumberString_ThrowsErrorForNegativeNumbers(int count)
        {
            var ints = _fixture.CreateManyIntsInRange(0, _maxInputValue, count);
            var negativeInts = _fixture.CreateManyNegativeInts(count);
            var both = ints.Concat(negativeInts).OrderBy(i => Math.Abs(i));
            var numbers = string.Join(_defaultDelimiter, both);
            var expected = _negativeNumberMessage + string.Join(_defaultDelimiter, negativeInts.OrderBy(i => Math.Abs(i)));

            var ex = Assert.Throws<NegativeNumberException>(() => _calculator.Add(numbers));
            var actual = ex.Message;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(NumberCounts))]
        public void Add_GivenUnknownNegativeNumberString_ThrowsErrorForNegativeNumbers(int count)
        {
            var negativeInts = _fixture.CreateManyNegativeInts(count);
            var negativeNumbers = string.Join(_defaultDelimiter, negativeInts);
            var expected = _negativeNumberMessage + negativeNumbers;

            var ex = Assert.Throws<NegativeNumberException>(() => _calculator.Add(negativeNumbers));
            var actual = ex.Message;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(NumberCounts))]
        public void Add_GivenUnknownNumberStringWithDefaultDelimiter_ReturnsSumOfNumbers(int count)
        {
            var ints = _fixture.CreateManyIntsInRange(0, _maxInputValue, count);
            var numbers = string.Join(_defaultDelimiter, ints);
            var expected = ints.Sum();

            var actual = _calculator.Add(numbers);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(NumberCounts))]
        public void Add_GivenUnknownNumberStringWithDefaultAndNewLineDelimiters_ReturnsSumOfNumbers(int count)
        {
            var ints = _fixture.CreateManyIntsInRange(0, _maxInputValue, count).ToArray();
            var numbers = TestDataGenerator.GetNumberString(ints, new[] { _defaultDelimiter[0], _newLineDelimiter[0] });
            var expected = ints.Sum();
            
            var actual = _calculator.Add(numbers);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(NumberCounts))]
        public void Add_GivenUnknownNumberStringWithMultipleSpecifiedDelimiters_ReturnsSumOfNumbers(int count)
        {
            var ints = _fixture.CreateManyIntsInRange(0, _maxInputValue, count);
            var delimiters = _fixture.CreateManyNonDigitChars(count);
            var delimiterString = TestDataGenerator.GetDelimiterString(delimiters);
            var numberString = TestDataGenerator.GetNumberString(ints, delimiters);
            var input = delimiterString + numberString;
            var expected = ints.Sum();

            var actual = _calculator.Add(input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(NumberCounts))]
        public void Add_GivenUnknownNumberStringWithNewLineDelimiter_ReturnsSumOfNumbers(int count)
        {
            var ints = _fixture.CreateManyIntsInRange(0, _maxInputValue, count);
            var numbers = string.Join(_newLineDelimiter, ints);
            var expected = ints.Sum();

            var actual = _calculator.Add(numbers);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(NumberCounts))]
        public void Add_GivenUnknownNumberStringWithNewLineAndSpecifiedDelimiters_ReturnsSumOfNumbers(int count)
        {
            var ints = _fixture.CreateManyIntsInRange(0, _maxInputValue, count).ToArray();
            var delimiter = _fixture.CreateNonDigitChar();
            var numbers = TestDataGenerator.GetSingleCharDelimiterString(delimiter);
            numbers += TestDataGenerator.GetNumberString(ints, new[] { delimiter, _newLineDelimiter[0] });
            var expected = ints.Sum();
            
            var actual = _calculator.Add(numbers);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(NumberCounts))]
        public void Add_GivenUnknownNumberStringWithNumbersAboveAndBelowInputUpperLimit_ReturnsSumOfNumbersBelowLimit(int count)
        {
            var aboveLimitInts = _fixture.CreateManyIntsInRange(_maxInputValue + 1, int.MaxValue, count);
            var belowLimitInts = _fixture.CreateManyIntsInRange(0, _maxInputValue, count);
            var both = aboveLimitInts.Concat(belowLimitInts);
            var numbers = string.Join(_defaultDelimiter, both);
            var expected = belowLimitInts.Sum();

            var actual = _calculator.Add(numbers);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(NumberCounts))]
        public void Add_GivenUnknownNumberStringWithNumbersAboveInputUpperLimit_Returns0(int count)
        {
            var aboveLimitInts = _fixture.CreateManyIntsInRange(_maxInputValue + 1, int.MaxValue, count);
            var numbers = string.Join(_defaultDelimiter, aboveLimitInts);
            var expected = 0;

            var actual = _calculator.Add(numbers);

            Assert.That(actual, Is.EqualTo(expected));
        }
        
        [TestCaseSource(nameof(NumberCounts))]
        public void Add_GivenUnknownNumberStringWithSpecifiedDelimiter_ReturnsSumOfNumbers(int count)
        {
            var ints = _fixture.CreateManyIntsInRange(0, _maxInputValue, count);
            var delimiter = _fixture.CreateNonDigitChar();
            var numbers = TestDataGenerator.GetSingleCharDelimiterString(delimiter)
                + string.Join(delimiter.ToString(), ints);
            var expected = ints.Sum();

            var actual = _calculator.Add(numbers);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(NumberCounts))]
        public void Add_GivenUnknownNumberStringWithSpecifiedMultiCharDelimiter_ReturnsSumOfNumbers(int count)
        {
            var ints = _fixture.CreateManyIntsInRange(0, _maxInputValue, count);
            var delimiter = _fixture.CreateStringExcludingCharSequences(_illegalDelimiters);
            var numbers = TestDataGenerator.GetDelimiterString(delimiter)
                + string.Join(delimiter, ints);
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
