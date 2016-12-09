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
        public void Add_Returns(string numbers, int expectedSum) {
            var testCalculator = new Calculator();
            Assert.That(testCalculator.Add(numbers), Is.EqualTo(expectedSum));
        }
    }
}
