using ServicePayments;

namespace CodingExercise.AppLayer
{
    public interface IAbstractPmtFactory
    {
        IAbstractPmtGateway PaymentGatewayAmt(decimal amt);
    }

    public class AbstractPmtFactory : IAbstractPmtFactory
    {
        public IAbstractPmtGateway PaymentGatewayAmt(decimal pmt)
        {
            if (pmt < 20)
            {
                return new CheapPaymentGateway();
            }
            else if (pmt >= 21 && pmt < 500)
            {
                return new ExpensivePaymentGateway();
            }
            return new PremiumPayment();
        }
    }
}
