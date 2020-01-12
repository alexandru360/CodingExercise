using ServicePayments;

namespace ServicePayments
{
    public class ExpensivePaymentGateway : IExpensivePaymentGateway
    {
        public bool ProcessPayment(decimal pmt)
        {
            return TryProcessingPmt(pmt);
        }

        private bool TryProcessingPmt(decimal pmt)
        {
            return true;
        }
    }
}
