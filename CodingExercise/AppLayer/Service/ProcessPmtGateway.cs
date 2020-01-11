using CodingExercise.Domain;
using CodingExercise.Infrastructure;
using Microsoft.Extensions.Logging;
using System;

namespace CodingExercise.AppLayer
{
    public interface IProcessPmtGateway
    {
        ProcessPayment PmtProcess(ProcessPayment pmt);
        bool ProcessPaymentThroughGateway(ProcessPayment pmt);
    }

    public class ProcessPmtGateway : IProcessPmtGateway
    {
        private readonly ILogger<ProcessPmtGateway> _logger;
        private IAbstractPmtFactory _pmtFactory;

        public ProcessPmtGateway(
            ILogger<ProcessPmtGateway> logger,
            IAbstractPmtFactory pmtFactory
            )
        {
            _logger = logger;
            _pmtFactory = pmtFactory;
        }

        public ProcessPayment PmtProcess(ProcessPayment pmt)
        {
            var oRet = new ProcessPayment();
            var success = ProcessPaymentThroughGateway(pmt);

            oRet = pmt;
            if(success)
                oRet.PaymentState.PmntState = ProcState.processed;
            else
                oRet.PaymentState.PmntState = ProcState.failed;

            return oRet;
        }

        public bool ProcessPaymentThroughGateway(ProcessPayment pmt)
        {
            var ret = false;

            try
            {
                var pmtGate = _pmtFactory.PaymentGatewayAmt(pmt.Amount);
                ret = pmtGate.ProcessPayment(pmt.Amount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                throw new Exception("Some error occured I can not process the payment, check the logs");
            }

            return ret;
        }
    }
}
