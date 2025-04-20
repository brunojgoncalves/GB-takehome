using AmountCalculator.API.Dtos;

namespace AmountCalculator.API.Calculators;

public class CalculationStrategyProvider : ICalculationStrategyProvider
{
    public ICalculationStrategy GetCalculatorStrategy(CalculationRequest calculationRequest)
    {
        if (calculationRequest.NetAmount.GetValueOrDefault() != 0)
        {
            return new CalculatorFromNetAmount();
        }

        if (calculationRequest.GrossAmount.GetValueOrDefault() != 0)
        {
            return new CalculatorFromGrossAmount();
        }

        if (calculationRequest.VATAmount.GetValueOrDefault() != 0)
        {
            return new CalculatorFromVATAmount();
        }

        throw new InvalidOperationException();
    }
}