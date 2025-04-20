using AmountCalculator.API.Dtos;
using AmountCalculator.API.Extensions;

namespace AmountCalculator.API.Calculators;

public class CalculatorFromGrossAmount : ICalculationStrategy
{
    public CalculationResponse Calculate(CalculationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request.GrossAmount);

        var grossAmount = request.GrossAmount!.Value;
        var netAmount = grossAmount.WithoutTax(request.Percentage);
        var vatAmount = request.Percentage.Percent().Of(netAmount);

        return new CalculationResponse()
        {
            NetAmount = AmountResultFactory.Create(netAmount, request.Percentage),
            VATAmount = vatAmount,
            GrossAmount = AmountResultFactory.CreateWithVAT(grossAmount, vatAmount)
        };
    }
}