using AmountCalculator.API.Dtos;
using AmountCalculator.API.Extensions;

namespace AmountCalculator.API.Calculators;

public class CalculatorFromNetAmount : ICalculationStrategy
{
    public CalculationResponse Calculate(CalculationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request.NetAmount);

        var netAmount = request.NetAmount!.Value;
        var vatAmount = request.Percentage.Percent().Of(netAmount);
        var grossAmount = netAmount.WithTax(request.Percentage);

        return new CalculationResponse()
        {
            NetAmount = AmountResultFactory.Create(netAmount, request.Percentage),
            VATAmount = vatAmount,
            GrossAmount = AmountResultFactory.CreateWithVAT(grossAmount, vatAmount)
        };
    }
}