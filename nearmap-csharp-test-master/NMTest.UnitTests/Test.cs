using FluentAssertions;
using Moq;
using NMTest.DataSource;
using NUnit.Framework;

namespace NMTest.UnitTests
{
    [TestFixture]
    public class Test
    {
        private Mock<DatabaseStore> _dataStore;
        private Mock<DistributedCacheStore> _distributedCacheStore;
        private Mock<CustomDataSource> _dataSource;

        [SetUp]
        public void Setup()
        {
            _dataStore = new Mock<DatabaseStore>();
            _distributedCacheStore = new Mock<DistributedCacheStore>();
            _dataSource = new Mock<CustomDataSource>(_distributedCacheStore.Object, _dataStore.Object);
        }

        [TestCase("key", "This is the first value")]
        [TestCase("key1", "This is the second value")]
        [TestCase("key2", 30)]
        public void GetValue_ReturnsCorrectValue_WhenValueIsFoundInDistributedCache(string key, object result)
        {
            _distributedCacheStore.Setup(store => store.GetValue(key)).Returns(result);

            var value = _dataSource.Object.GetValue(key);

            value.Should().Be(result);
        }

        [TestCase("key", null)]
        [TestCase("key1", null)]
        [TestCase("key2", null)]
        public void GetValue_ReturnsNull_WhenValueIsNotFoundInDistributedCache(string key, object result)
        {
            _distributedCacheStore.Setup(store => store.GetValue(It.IsAny<string>())).Returns(result);

            var value = _dataSource.Object.GetValue(key);

            value.Should().BeNull();
        }

        [TestCase("key", "This is the first value")]
        [TestCase("key1", "This is the second value")]
        [TestCase("key2", 30)]
        public void GetValue_ReturnsCorrectValue_WhenValueIsFoundInDatabase(string key, object result)
        {
            _dataStore.Setup(store => store.GetValue(It.IsAny<string>())).Returns(result);

            var value = _dataSource.Object.GetValue(key);

            value.Should().Be(result);
        }

        [TestCase("key", null)]
        [TestCase("key1", null)]
        [TestCase("key2", null)]
        public void GetValue_ReturnsNull_WhenValueIsNotFoundInDatabase(string key, object result)
        {
            _dataStore.Setup(store => store.GetValue(It.IsAny<string>())).Returns(result);

            var value = _dataSource.Object.GetValue(key);

            value.Should().BeNull();
        }

        [TestCase("key", "This is the first value")]
        [TestCase("key1", "This is the second value")]
        [TestCase("key2", 30)]
        public void GetValue_ReturnsCorrectValue_WhenValueIsFoundInLocalCache(string key, object result)
        {
            _dataSource.Setup(store => store.GetValueFromLocalCache(It.IsAny<string>())).Returns(result);

            var value = _dataSource.Object.GetValue(key);

            value.Should().Be(result);
        }

        [TestCase("key", null)]
        [TestCase("key1", null)]
        [TestCase("key2", null)]
        public void GetValue_ReturnsNull_WhenValueIsNotFoundInLocalCache(string key, object result)
        {
            _dataSource.Setup(store => store.GetValueFromLocalCache(It.IsAny<string>())).Returns(result);

            var value = _dataSource.Object.GetValue(key);

            value.Should().BeNull();
        }
    }
}
