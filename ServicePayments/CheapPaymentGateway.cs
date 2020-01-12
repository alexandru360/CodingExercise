using ServicePayments;

namespace ServicePayments
{
    public class CheapPaymentGateway : ICheapPaymentGateway
    {
        public bool ProcessPayment(decimal pmt)
        {
            return true;
        }
    }
}
