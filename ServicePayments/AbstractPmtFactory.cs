using Microsoft.Extensions.DependencyInjection;

namespace ServicePayments
{
    public class AbstractPmtFactory : IAbstractPmtFactory
    {
        private IPremiumPayment _premium;
        private IExpensivePaymentGateway _expensive;
        private ICheapPaymentGateway _cheap;

        public AbstractPmtFactory()
        {
            var services = new ServiceCollection();

            services.AddScoped<IPremiumPayment, PremiumPayment>();
            services.AddScoped<IExpensivePaymentGateway, ExpensivePaymentGateway>();
            services.AddScoped<ICheapPaymentGateway, CheapPaymentGateway>();

            var serviceProvider = services.BuildServiceProvider();

            _premium = serviceProvider.GetService<IPremiumPayment>();
            _expensive = serviceProvider.GetService<IExpensivePaymentGateway>();
            _cheap = serviceProvider.GetService<ICheapPaymentGateway>();
        }

        public IAbstractPmtGateway PaymentGatewayAmt(decimal pmt)
        {
            if (pmt < 20)
            {
                return _cheap;
            }
            else if (pmt >= 21 && pmt < 500)
            {
                if (_expensive != null) return _expensive;
                else return _cheap;
            }
            return _premium;
        }
    }
}
