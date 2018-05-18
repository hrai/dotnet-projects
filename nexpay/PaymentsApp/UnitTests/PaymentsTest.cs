using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PaymentsApp.Models;
using PaymentsApp.Services;

namespace PaymentsApp.UnitTests
{
    [TestFixture]
    public class PaymentsTest
    {
        private IPaymentService _paymentService;
        private Mock<IHostingEnvironment> _hostingEnvironment;
        private Mock<ILogger<PaymentService>> _logger;
        private string _pathToFile;

        [SetUp]
        public void Setup()
        {
            _hostingEnvironment = new Mock<IHostingEnvironment>();
            _logger = new Mock<ILogger<PaymentService>>();

            _paymentService = new PaymentService(_hostingEnvironment.Object, _logger.Object);
            _pathToFile = Path.GetTempPath();
        }

        [Test]
        public void ProcessPayment_ThrowsException_IfDirectoryNotFound()
        {
            _hostingEnvironment.Setup(env => env.ContentRootPath).Returns("dummy_path");

            Assert.Throws(typeof(DirectoryNotFoundException), () => _paymentService.ProcessPayment(GetPaymentDetails()));
        }

        [Test]
        public void ProcessPayment_ReturnFalse_IfAccountNumberIsNegative()
        {
            var successfulPayment = _paymentService.ProcessPayment(GetInvalidPaymentDetails());

            Assert.IsFalse(successfulPayment);
        }

        [Test]
        public void ProcessPayment_ReturnTrue_IfAccountNumberIsPositive()
        {
            _hostingEnvironment.Setup(env => env.ContentRootPath).Returns(_pathToFile);
            var successfulPayment = _paymentService.ProcessPayment(GetPaymentDetails());

            Assert.IsTrue(successfulPayment);
        }

        [Test]
        public void ProcessPayment_ReturnFalse_IfBsbIsNegative()
        {
            var successfulPayment = _paymentService.ProcessPayment(GetInvalidPaymentDetails());

            Assert.IsFalse(successfulPayment);
        }

        [Test]
        public void ProcessPayment_ReturnTrue_IfBsbIsPositive()
        {
            _hostingEnvironment.Setup(env => env.ContentRootPath).Returns(_pathToFile);
            var successfulPayment = _paymentService.ProcessPayment(GetPaymentDetails());

            Assert.IsTrue(successfulPayment);
        }

        [Test]
        public void ProcessPayment_ReturnFalse_IfAmountIsNegative()
        {
            var successfulPayment = _paymentService.ProcessPayment(GetInvalidPaymentDetails());

            Assert.IsFalse(successfulPayment);
        }

        [Test]
        public void ProcessPayment_ReturnTrue_IfAmountIsPositive()
        {
            _hostingEnvironment.Setup(env => env.ContentRootPath).Returns(_pathToFile);
            var successfulPayment = _paymentService.ProcessPayment(GetPaymentDetails());

            Assert.IsTrue(successfulPayment);
        }

        private PaymentDetails GetPaymentDetails()
        {
            var paymentDetails = new PaymentDetails
            {
                AccountName = "Hangjit",
                AccountNumber = 1234567,
                Bsb = 123456,
                PaymentAmount = 234,
                Reference = "Test ref"
            };

            return paymentDetails;
        }

        private PaymentDetails GetInvalidPaymentDetails()
        {
            var paymentDetails = new PaymentDetails
            {
                AccountName = "Hangjit",
                AccountNumber = -123456,
                Bsb = 2378,
                PaymentAmount = -72,
                Reference = "Test ref"
            };

            return paymentDetails;
        }
    }
}
