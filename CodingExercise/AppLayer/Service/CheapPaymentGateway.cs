namespace CodingExercise.AppLayer
{
    public interface ICheapPaymentGateway
    {
        bool ProcessPmt(decimal amt);
    }

    public class CheapPaymentGateway : ICheapPaymentGateway
    {
        public bool ProcessPmt(decimal amt)
        {
            return true;
        }
    }
}
