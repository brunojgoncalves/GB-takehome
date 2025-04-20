using AmountCalculator.API.Dtos;

namespace AmountCalculator.API.Calculators;

public interface ICalculationStrategy
{
    CalculationResponse Calculate(CalculationRequest request);
}