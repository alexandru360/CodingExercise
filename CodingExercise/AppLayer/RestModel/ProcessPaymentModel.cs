using FluentValidation;
using System;
using System.Text.RegularExpressions;

namespace CodingExercise.RestModel
{
    public class ProcessPaymentModel
    {
        public long CreditCardNumber { get; set; }
        public string CardHolder { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        public double Amount { get; set; }
    }

    public class ProcessPaymentValidator : AbstractValidator<ProcessPaymentModel>
    {
        public ProcessPaymentValidator()
        {
            RuleFor(p => p.CreditCardNumber).NotNull().NotEmpty().Must(CreditCardNumberField).WithMessage("Credit card number should be a valid CreditCard No");
            RuleFor(p => p.CardHolder).NotNull().NotEmpty().WithMessage("Card holder should be completed");
            RuleFor(p => p.ExpirationDate).NotNull().NotEmpty().Must(BeValidDate).WithMessage("Exp date is in format: yyyy-MM-ddT00:00:00");
            RuleFor(p => p.SecurityCode).Must(SecurityCodeField).WithMessage("Security code is a 3 digit string");
            RuleFor(p => p.Amount).GreaterThan(0).WithMessage("Amount is decimal grater than 0");
        }

        private bool CreditCardNumberField(long arg)
        {
            Regex expression = new Regex(@"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$");
            return expression.IsMatch(arg.ToString());
        }

        private bool SecurityCodeField(string arg)
        {
            return arg.Length == 3;
        }

        private bool BeValidDate(DateTime arg)
        {
            return arg.Date >= DateTime.Now.Date;
        }
    }
}
