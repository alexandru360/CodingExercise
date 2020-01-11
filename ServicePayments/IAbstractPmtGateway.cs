namespace ServicePayments
{
    public interface IAbstractPmtGateway
    {
        bool ProcessPayment(decimal pmt);
    }
}