using CodingExercise.Domain;
using CodingExercise.Infrastructure;
using Microsoft.Extensions.Logging;
using System;

namespace CodingExercise.AppLayer
{
    public interface IProcessPmtGateway
    {
        ProcessPayment PmtProcess(ProcessPayment pmt);
        bool PremiumPaymentService(decimal amt);
    }

    public class ProcessPmtGateway : IProcessPmtGateway
    {
        private readonly ILogger<ProcessPmtGateway> _logger;
        private ICheapPaymentGateway _cheapPaymentGateway;
        private IExpensivePaymentGateway _expensivePaymentGateway;

        public ProcessPmtGateway(
            ILogger<ProcessPmtGateway> logger,
            ICheapPaymentGateway cheapPaymentGateway,
            IExpensivePaymentGateway expensivePaymentGateway
            )
        {
            _logger = logger;
            _cheapPaymentGateway = cheapPaymentGateway;
            _expensivePaymentGateway = expensivePaymentGateway;
        }

        public ProcessPayment PmtProcess(ProcessPayment pmt)
        {
            var oRet = new ProcessPayment();
            var success = false;
            try
            {
                if (pmt.Amount < 20)
                {
                    success = _cheapPaymentGateway.ProcessPmt(pmt.Amount);
                }
                else if (pmt.Amount >= 21 && oRet.Amount < 500)
                {
                    if (_expensivePaymentGateway != null)
                    {
                        success = _expensivePaymentGateway.ProcessPmt(pmt.Amount);
                    }
                    else
                    {
                        int retry = 0;
                        while (retry <= 1 && !success) {
                            success = _cheapPaymentGateway.ProcessPmt(pmt.Amount);
                        }
                    }
                }
                else if (pmt.Amount >= 500)
                {
                    var retry = 1;
                    while (retry <= 3)
                    {
                        if (!PremiumPaymentService(pmt.Amount))
                        {
                            retry = retry + 1;
                            continue;
                        }
                        break;
                    }
                }

                oRet = pmt;
                if(success)
                    oRet.PaymentState.PmntState = ProcState.processed;
                else
                    oRet.PaymentState.PmntState = ProcState.failed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                throw new Exception("Some error occured I can not process the payment, check the logs");
            }
            return oRet;
        }

        public bool PremiumPaymentService(decimal amt)
        {
            return true;
        }
    }
}
