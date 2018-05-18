using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaymentsApp.Models;

namespace PaymentsApp.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger _logger;

        public PaymentService(IHostingEnvironment hostingEnvironment, ILogger<PaymentService> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }

        public bool ProcessPayment(PaymentDetails paymentDetails)
        {
            try
            {
                if (CheckDataValidity(paymentDetails))
                {
                    var path = GetFilePathToStoreData();
                    string json = JsonConvert.SerializeObject(paymentDetails, Formatting.Indented);

                    File.AppendAllText(path, $"{Environment.NewLine}{json}");

                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                throw;
            }

            return false;
        }

        private bool CheckDataValidity(PaymentDetails paymentDetails)
        {
            var regex = new Regex("^[0-9]{6,6}$");
            
            if (!regex.IsMatch(paymentDetails.Bsb.ToString()))
            {
                _logger.LogError("Invalid BSB number");
                return false;
            }

            if (paymentDetails.AccountNumber < 0)
            {
                _logger.LogError("Invalid AccountNumber");
                return false;
            }

            if (paymentDetails.PaymentAmount <= 0)
            {
                _logger.LogError("Negative payment amount");
                return false;
            }

            return true;
        }

        private string GetFilePathToStoreData()
        {
            var fileName = "payment-details.txt";
            return $"{Path.GetFullPath(_hostingEnvironment.ContentRootPath)}{Path.DirectorySeparatorChar}{fileName}";
        }
    }
}
