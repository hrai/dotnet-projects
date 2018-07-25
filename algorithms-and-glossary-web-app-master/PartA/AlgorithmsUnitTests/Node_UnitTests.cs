using FluentAssertions;
using NUnit.Framework;
using PartA;

namespace AlgorithmsUnitTests
{
    [TestFixture]
    public class Node_UnitTests
    {
        private Node<int> _node;
        private CustomLinkedList<int> _customLinkedList;

        [SetUp]
        public void Setup()
        {
            _node = new Node<int>(null, 2);
            _customLinkedList= new CustomLinkedList<int>();
        }

        [Test]
        public void Count_ReturnsCorrectValue()
        {
            _customLinkedList.Add(1);
            _customLinkedList.Add(2);
            _customLinkedList.Add(3);

            _customLinkedList.Count().Should().Be(3);
        }

        [Test]
        public void GetFifthElementFromTailEnd_ReturnsCorrectValue_WhenThereIsLessThanFiveElements()
        {
            _customLinkedList.Add(1);
            _customLinkedList.Add(3);

            _customLinkedList.GetFifthElementFromTailEnd().Should().Be(1);
        }

        [Test]
        public void GetFifthElementFromTailEnd_ReturnsCorrectValue_WhenThereIsFiveElements()
        {
            _customLinkedList.Add(7);
            _customLinkedList.Add(2);
            _customLinkedList.Add(3);
            _customLinkedList.Add(4);
            _customLinkedList.Add(5);

            _customLinkedList.GetFifthElementFromTailEnd().Should().Be(7);
        }

        [Test]
        public void GetFifthElementFromTailEnd_ReturnsCorrectValue_WhenThereIsMoreThanFiveElements()
        {
            _customLinkedList.Add(8);
            _customLinkedList.Add(1);
            _customLinkedList.Add(2);
            _customLinkedList.Add(3);
            _customLinkedList.Add(4);
            _customLinkedList.Add(5);

            _customLinkedList.GetFifthElementFromTailEnd().Should().Be(1);
        }
    }
}
