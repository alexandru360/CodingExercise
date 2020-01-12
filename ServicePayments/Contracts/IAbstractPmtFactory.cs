namespace ServicePayments
{
    public interface IAbstractPmtFactory
    {
        IAbstractPmtGateway PaymentGatewayAmt(decimal amt);
    }
}
