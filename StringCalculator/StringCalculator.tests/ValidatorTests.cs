using System.Linq;
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
            Assert.That(
                () => { testValidator.ValidateNumbers(numbers); },
                Throws.ArgumentException.With.Property("Message").EqualTo(exceptionMessage));
        }
    }
}
