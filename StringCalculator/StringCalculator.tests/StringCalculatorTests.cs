using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.tests
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
        [TestCase("1\n2,3", 6)]
        [TestCase("//;\n1;2", 3)]
        public void Add_Returns(string input, int expectedSum) {
            var testCalculator = new Calculator();
            Assert.That(testCalculator.Add(input), Is.EqualTo(expectedSum));
        }

        [Test]
        [TestCase("-1", "Negatives not allowed: -1")]
        public void Add_ThrowsException(string input, string exceptionMessage) {
            var testCalculator = new Calculator();
            Assert.That(delegate { testCalculator.Add(input); }, Throws.ArgumentException.With.Property("Message").EqualTo(exceptionMessage));
        }
    }
}
