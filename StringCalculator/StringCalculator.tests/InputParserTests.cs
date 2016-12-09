using System.Collections.Generic;
using NUnit.Framework;

namespace StringCalculator.Tests
{
    [TestFixture]
    public class InputParserTests
    {
        [Test]
        [TestCase("", new int[0])]
        [TestCase("1", new int[] { 1 })]
        [TestCase("0", new int[] { 0 })]
        [TestCase("1,1", new int[] { 1, 1 })]
        [TestCase("1,1,1,2", new int[] { 1, 1, 1, 2 })]
        [TestCase("1\n2,3", new int[] { 1, 2, 3 })]
        [TestCase("//;\n1;2", new int[] { 1, 2 })]
        public void ParseToInts_Returns(string input, IEnumerable<int> expectedIEnumerable)
        {
            var testParser = new InputParser();
            Assert.That(testParser.ParseToInts(input), Is.EqualTo(expectedIEnumerable));
        }
    }
}
