using ServicePayments;

namespace CodingExercise.AppLayer
{
    public class PremiumPayment : IAbstractPmtGateway
    {
        private int retry = 0;
        public bool ProcessPayment(decimal pmt)
        {
            var sendPmt = false;
            while (retry <= 2 && !sendPmt)
            {
                sendPmt = PremiumPaymentService(pmt);
                if (sendPmt) continue;
                else break;
            }
            return sendPmt;
        }

        private bool PremiumPaymentService(decimal pmt)
        {
            return true;
        }
    }
}
