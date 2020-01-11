using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingExercise.AppLayer
{
    public interface IExpensivePaymentGateway
    {
        bool ProcessPmt(decimal amt);

    }

    public class ExpensivePaymentGateway : IExpensivePaymentGateway
    {
        public bool ProcessPmt(decimal amt)
        {
            return true;
        }
    }
}
