using CodingExercise.Domain;
using CodingExercise.Infrastructure;
using System;
using System.Collections.Generic;

namespace CodingExercise.AppLayer
{
    public interface IPmtService
    {
        IEnumerable<ProcessPayment> GetAll();
        ProcessPayment GetById(int id);
        ProcessPayment Create(ProcessPayment pmt);
    }

    public class PmtService : IPmtService
    {
        private DataContext _context;
        private IProcessPmtGateway _pmtGate;
        private IProcessPmtGateway _processPmtGate;

        public PmtService(
            DataContext context,
            IProcessPmtGateway pmtGate,
            IProcessPmtGateway processPmtGate
            )
        {
            _context = context;
            _pmtGate = pmtGate;
            _processPmtGate = processPmtGate;
        }

        public ProcessPayment Create(ProcessPayment pmt)
        {
            if (_pmtGate.PmtProcess(pmt).PaymentState.PmntState != ProcState.failed)
            {
                _context.ProcessPayments.Add(pmt);
                _context.SaveChanges();

                return pmt;
            }
            else
            {
                throw new Exception("Payment unprocessed");
            }
        }

        public IEnumerable<ProcessPayment> GetAll()
        {
            return _context.ProcessPayments;
        }

        public ProcessPayment GetById(int id)
        {
            return _context.ProcessPayments.Find(id);
        }
    }
}
