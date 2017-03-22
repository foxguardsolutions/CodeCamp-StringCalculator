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
        private Fixture _fixture;
        private StringCalculator _calculator;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _calculator = new StringCalculator();
        }
        
        [Test]
        public void Add_GivenEmptyString_Returns0()
        {
            var emptyString = "";
            var expected = 0;

            var actual = _calculator.Add(emptyString);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Add_GivenSingleNumberString_ReturnsIntegerValue()
        {
            var expected = _fixture.Create<int>();

            var actual = _calculator.Add(expected.ToString());

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Add_GivenTwoNumberString_ReturnsSumOfNumbers()
        {
            var number1 = _fixture.Create<int>();
            var number2 = _fixture.Create<int>();
            var numbers = number1 + "," + number2;
            var expected = number1 + number2;

            var actual = _calculator.Add(numbers);

            Assert.That(actual, Is.EqualTo(expected));
        }       
    }
}
