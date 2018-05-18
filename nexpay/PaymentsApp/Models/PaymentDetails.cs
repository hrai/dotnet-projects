using System.ComponentModel.DataAnnotations;

namespace PaymentsApp.Models
{
    public class PaymentDetails
    {
        [Required]
        [Range(100000, 999999)]
        public int Bsb { get; set; }

        [Required]
        public int AccountNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string AccountName { get; set; }

        [Required]
        public decimal PaymentAmount { get; set; }

        [StringLength(100)]
        public string Reference { get; set; }
    }
}