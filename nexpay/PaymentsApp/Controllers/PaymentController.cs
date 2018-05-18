using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentsApp.Models;
using PaymentsApp.Services;

namespace PaymentsApp.Controllers
{
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger _logger;

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpPost("[action]")]
        public ActionResult SubmitPayments([FromBody] PaymentDetails paymentDetails)
        {
            if (TryValidateModel(paymentDetails))
            {
                _paymentService.ProcessPayment(paymentDetails);
                return Ok();
            }

            const string errorMessage = "Invalid model.";
            _logger.LogError(errorMessage);

            return BadRequest(errorMessage);
        }
    }
}
