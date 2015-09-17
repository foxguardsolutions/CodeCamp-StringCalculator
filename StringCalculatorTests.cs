using System;
using NUnit.Framework;

namespace StringCalculator
{
    [TestFixture]
    public class StringCalculatorTests
    {
        [Test]
        public void AddReturnsZeroForEmptyString()
        {
            Assert.AreEqual(0, StringCalculator.Add(string.Empty));
        }

        [TestCase("1", 1)]
        [TestCase("2", 2)]
        [TestCase("30", 30)]
        public void AddReturnsIntegerValueWhenGivenOneArgument(string stringValue, int intValue)
        {
            Assert.AreEqual(intValue, StringCalculator.Add(stringValue));
        }

        [TestCase("1,2", 3)]
        [TestCase("2,3,4", 9)]
        public void AddReturnsSumOfIntegersWhenMultiplesAreGiven(string numbers, int sum)
        {
            Assert.AreEqual(sum, StringCalculator.Add(numbers));
        }

        [TestCase("1\n2,3", 6)]
        public void AddReturnsSumOfIntegersWithCommaAndNewlineDelimiters(string numbers, int sum)
        {
            Assert.AreEqual(sum, StringCalculator.Add(numbers));
        }

        [TestCase("//$X\n1$2X3", 6)]
        [TestCase("//^&\n4^5&6", 15)]
        public void AddAllowsChangingDelimiter(string numbers, int sum)
        {
            Assert.AreEqual(sum, StringCalculator.Add(numbers));
        }

        [TestCase("-1", 0)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddDoesNotAllowNegatives(string numbers, int sum)
        {
            Assert.AreEqual(sum, StringCalculator.Add(numbers));
        }

        [TestCase("1,1001", 1)]
        [TestCase("1005,10000", 0)]
        public void AddIgnoresNumbersGreaterThanOneThousand(string numbers, int sum)
        {
            Assert.AreEqual(sum, StringCalculator.Add(numbers));
        }

        [TestCase("//[xxx]\n1xxx5", 6)]
        public void DelemitersCanBeOfAnyLength(string numbers, int sum)
        {
            Assert.AreEqual(sum, StringCalculator.Add(numbers));
        }

        [TestCase("//[xx][yy][zz]abc\n1xx2yy3", 6)]
        public void AddAllowsMultipleMultiDigitDelimiters(string numbers, int sum)
        {
            Assert.AreEqual(sum, StringCalculator.Add(numbers));
        }
    }
}
