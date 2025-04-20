using AmountCalculator.API.Dtos;

namespace AmountCalculator.API.Calculators;

public interface ICalculationStrategyProvider
{
    ICalculationStrategy GetCalculatorStrategy(CalculationRequest calculationRequest);
}