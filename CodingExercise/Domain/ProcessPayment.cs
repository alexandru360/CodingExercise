using CodingExercise.Infrastructure;
using System;

namespace CodingExercise.Domain
{
    public class ProcessPayment
    {
        public int Id { get; set; }
        public long CreditCardNumber { get; set; }
        public string CardHolder { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        public decimal Amount { get; set; }

        public PaymentState PaymentState { get; set; } = new PaymentState();
    }
}
