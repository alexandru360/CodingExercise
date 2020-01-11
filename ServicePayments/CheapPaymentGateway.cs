using ServicePayments;

namespace CodingExercise.AppLayer
{
    public class CheapPaymentGateway : IAbstractPmtGateway
    {
        public bool ProcessPayment(decimal pmt)
        {
            return true;
        }
    }
}
