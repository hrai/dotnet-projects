using FluentAssertions;
using NUnit.Framework;
using PartA;

namespace AlgorithmsUnitTests
{
    [TestFixture]
    public class String_UnitTests
    {
        private StringUtil _stringUtil;

        [SetUp]
        public void Setup()
        {
            _stringUtil = new StringUtil();
        }

        [TestCase("input string", "gnirts tupni")]
        [TestCase("renewtrak", "kartwener")]
        [TestCase("_renewtrak", "kartwener_")]
        [TestCase("_renew.trak", "kart.wener_")]
        [TestCase("_renew*trak", "kart*wener_")]
        public void Reverse_ReturnsReverseString(string input, string output)
        {
            _stringUtil.Reverse(input).Should().Be(output);
        }
    }
}
