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
        public void Add_Returns(string numbers, int expectedSum) {
            var testCalculator = new Calculator();
            Assert.That(testCalculator.Add(numbers), Is.EqualTo(expectedSum));
        }
    }
}
