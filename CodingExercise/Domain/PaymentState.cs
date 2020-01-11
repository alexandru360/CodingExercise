using CodingExercise.Domain;

namespace CodingExercise.Infrastructure
{
    public class PaymentState
    {
        public int Id { get; set; }
        //public string PmntState { get; set; }
        public ProcState PmntState { get; set; }

        public int ProcessPaymentId { get; set; }
        public ProcessPayment ProcessPayment { get; set; }
    }

    public enum ProcState
    {
        pending, processed, failed
    }
}