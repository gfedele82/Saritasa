using Common;
using FluentValidation;
using Models.Request;
using System;

namespace Engine.Validators
{
    public class BrokerValidator : AbstractValidator<RequestRates>
    {
        public BrokerValidator()
        {
            RuleFor(x => x.moneyUsd).NotNull().WithMessage(ExcepcionsMenssages.MoneyNotNull);
            RuleFor(x => x.endDate).NotNull().WithMessage(ExcepcionsMenssages.DateNotNull);
            RuleFor(x => x.startDate).NotNull().WithMessage(ExcepcionsMenssages.DateNotNull);
            RuleFor(x => x.moneyUsd).GreaterThan(0).WithMessage(ExcepcionsMenssages.MoneyNotLessThan0);
            RuleFor(x => (x.endDate - x.startDate).Days).Must(y => y > 0 && y < 61).WithMessage(ExcepcionsMenssages.DatePatron);
            RuleFor(x => x.endDate).Must(y => y <= DateTime.Now).WithMessage(ExcepcionsMenssages.DatePatron);
            RuleFor(x => x.startDate).Must(y => y <= DateTime.Now).WithMessage(ExcepcionsMenssages.DatePatron);
        }

    }
}
