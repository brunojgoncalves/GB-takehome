using AmountCalculator.API.Dtos;
using AmountCalculator.API.Extensions;

namespace AmountCalculator.API.Calculators;

public class CalculatorFromVATAmount : ICalculationStrategy
{
    public CalculationResponse Calculate(CalculationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request.VATAmount);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(request.VATAmount.GetValueOrDefault(0), 0, nameof(request.VATAmount));

        var vatAmount = request.VATAmount!.Value;
        var netAmount = vatAmount.NetAmountFromVATAndVATPercent(request.Percentage);
        var grossAmount = netAmount + vatAmount;

        return new CalculationResponse()
        {
            NetAmount = AmountResultFactory.Create(netAmount, request.Percentage),
            VATAmount = vatAmount,
            GrossAmount = AmountResultFactory.CreateWithVAT(grossAmount, vatAmount)
        };
    }
}