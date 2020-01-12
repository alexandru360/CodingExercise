using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ServicePayments;

namespace Tests
{
    public class Tests
    {
        private IAbstractPmtFactory matchRepository;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddScoped<IAbstractPmtFactory, AbstractPmtFactory>();

            var serviceProvider = services.BuildServiceProvider();

            matchRepository = serviceProvider.GetService<IAbstractPmtFactory>();
        }

        [Test]
        public void CheckLimitTwenty()
        {
            var pmt = matchRepository.PaymentGatewayAmt(17);
            Assert.IsTrue(pmt.GetType() == typeof(CheapPaymentGateway));
        }

        [Test]
        public void CheckBetweenTwentyAndFiveHundred()
        {
            var pmt = matchRepository.PaymentGatewayAmt(50);
            Assert.IsTrue(pmt.GetType() == typeof(ExpensivePaymentGateway));
        }

        [Test]
        public void CheckAboveFiveHundred()
        {
            var pmt = matchRepository.PaymentGatewayAmt(550);
            Assert.IsTrue(pmt.GetType() == typeof(PremiumPayment));
        }
    }
}