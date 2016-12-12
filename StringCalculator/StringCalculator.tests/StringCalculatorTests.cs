using System;
using NUnit.Framework;

namespace StringCalculator.Tests
{
    [TestFixture]
    public class StringCalculatorTests
    {
        [Test]
        [TestCase("", 0)]
        [TestCase("1", 1)]
        [TestCase("0", 0)]
        [TestCase("1,1", 2)]
        [TestCase("1,1,1,2", 5)]
        [TestCase("2,1001", 2)]
        public void Add_Returns(string input, int expectedSum)
        {
            var testCalculator = new Calculator();
            var calculatedSum = testCalculator.Add(input);
            Assert.That(calculatedSum, Is.EqualTo(expectedSum));
        }

        [Test]
        [TestCase("1,-2,5,-5,-1", "Negatives not allowed: -2, -5, -1")]
        public void Add_ThrowsException(string input, string exceptionMessage)
        {
            var testCalculator = new Calculator();
            Assert.Throws<ArgumentException>(() => { testCalculator.Add(input); }, exceptionMessage);
        }
    }
}
