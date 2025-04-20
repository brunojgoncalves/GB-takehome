using AmountCalculator.API.Dtos;
using FluentValidation;

namespace AmountCalculator.API.Validators;

public class CalculationRequestValidator : AbstractValidator<CalculationRequest>, IValidator<CalculationRequest>
{
    public CalculationRequestValidator()
    {
        RuleFor(x => x.Percentage)
            .Must(x => x == 10 || x == 13 || x == 20)
            .WithMessage("Percentage must be 10, 13 or 20");
        RuleFor(r => r)
            .Must(OnlyOneAmountFilled)
            .WithMessage("Only one of the 'NetAmount', 'VATAmount', 'GrossAmount' must be provided");
    }

    private bool OnlyOneAmountFilled(CalculationRequest request)
    {
        var filled = 0;

        if (request.NetAmount.GetValueOrDefault(0) != 0) filled++;
        if (request.VATAmount.GetValueOrDefault(0) != 0) filled++;
        if (request.GrossAmount.GetValueOrDefault(0) != 0) filled++;

        return filled == 1;
    }
}