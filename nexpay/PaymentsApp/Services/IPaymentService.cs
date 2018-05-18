using PaymentsApp.Models;

namespace PaymentsApp.Services
{
    public interface IPaymentService
    {
        bool ProcessPayment(PaymentDetails paymentDetails);
    }
}