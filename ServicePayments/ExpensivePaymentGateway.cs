using ServicePayments;

namespace CodingExercise.AppLayer
{
    public class ExpensivePaymentGateway : IAbstractPmtGateway
    {
        private int retry = 0;
        public bool ProcessPayment(decimal pmt)
        {
            var sendPmt = false;
            while(retry <= 1 && !sendPmt)
            {
                sendPmt = TryProcessingPmt(pmt);
                retry = retry + 1;
            }
            return sendPmt;
        }

        private bool TryProcessingPmt(decimal pmt)
        {
            return true;
        }
    }
}
