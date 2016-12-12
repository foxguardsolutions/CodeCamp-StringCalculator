using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace StringCalculator.Tests
{
    [TestFixture]
    public class ValidatorTests
    {
        [Test]
        [TestCase(new int[] { -1 }, "Negatives not allowed: -1")]
        [TestCase(new int[] { -2 }, "Negatives not allowed: -2")]
        [TestCase(new int[] { 1, -2, 5, -5, -1 }, "Negatives not allowed: -2, -5, -1")]
        public void ValidateNumbers_ThrowsException(int[] numbers, string exceptionMessage)
        {
            var testValidator = new Validator();
            Assert.Throws<ArgumentException>(() => { testValidator.Validate(numbers); }, exceptionMessage);
        }

        [Test]
        [TestCase(new int[] { 1, 2 }, new int[] { 1, 2 })]
        [TestCase(new int[] { 2, 1001 }, new int[] { 2 })]
        public void ValidateNumbers_Returns(int[] numbers, IEnumerable<int> expectedResult)
        {
            var testValidator = new Validator();
            var validatedNumbers = testValidator.Validate(numbers);
            Assert.That(validatedNumbers, Is.EqualTo(expectedResult));
        }
    }
}
