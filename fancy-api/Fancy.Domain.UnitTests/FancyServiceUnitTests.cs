using System;
using FluentAssertions;
using NUnit.Framework;

namespace Fancy.Domain.UnitTests
{
    [TestFixture]
    public class FancyServiceUnitTests
    {
        private FancyService _fancyService;

        [SetUp]
        public void Setup()
        {
            _fancyService = new FancyService();
        }

        [Test]
        public void Get_Me_This()
        {
            //Arrange
            blah

            //Act


            //Assert


        }

        [TestCase(0, 0)]
        [TestCase(2, 1)]
        [TestCase(10, 55)]
        public void GetFibonacciNumberForPositiveIndex_ReturnsCorrectValue_WhenPassedAValue(long input, long result)
        {
            _fancyService.GetFibonacciNumberForPositiveIndex(input).Should().Be(result);
        }

        [TestCase(0, 0)]
        [TestCase(-2, -1)]
        [TestCase(-10, -55)]
        public void GetFibonacciNumberForNegativeIndex_ReturnsCorrectValue_WhenPassedAValue(long input, long result)
        {
            _fancyService.GetFibonacciNumberForNegativeIndex(input).Should().Be(result);
        }

        [TestCase(1, 2, 3, "Error")]
        [TestCase(7, 5, 6, "Scalene")]
        [TestCase(2, 2, 3, "Isosceles")]
        [TestCase(3, 3, 3, "Equilateral")]
        [TestCase(3, 3, 9, "Error")]
        [TestCase(0, 0, 0, "Error")]
        [TestCase(1, 3, 0, "Error")]
        [TestCase(-1, 2, 3, "Error")]
        [TestCase(2147483644, 2147483644, 2147483647, "Isosceles")]
        public void GetTriangleType_ReturnsCorrectTriangleType_ForValidInput(int a, int b, int c, string triangleType)
        {
            _fancyService.GetTriangleType(a, b, c).Should().BeEquivalentTo(triangleType);
        }

        [TestCase("ab,", ",ba")]
        [TestCase("abcde", "edcba")]
        [TestCase("-10", "01-")]
        [TestCase(null, "")]
        [TestCase("", "")]
        public void GetReverseWords_ReturnsReverseWords(string input, string result)
        {
            _fancyService.GetReverseWords(input).Should().BeEquivalentTo(result);
        }

        [TestCase("f20a636b-60f5-41c3-a671-c307af5ee56e")]
        public void GetToken_ReturnsEmptyGuid(string result)
        {
            _fancyService.GetToken().Should().BeEquivalentTo(result);
        }
    }
}
