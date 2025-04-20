namespace AmountCalculator.API.Dtos;

public static class AmountResultFactory
{
    private const int _amountDecimalPlaces = 2;
    private const int _multiplierDecimalPlaces = 6;

    public static AmountResult Create(decimal value, decimal percentage)
    {
        var vatPercentage = percentage / 100;
        return new AmountResult(
            Math.Round(value, _amountDecimalPlaces),
            Math.Round(vatPercentage, _multiplierDecimalPlaces));
    }

    public static AmountResult CreateWithVAT(decimal value, decimal vatAmount)
    {
        if (value == 0 && vatAmount == 0)
        {
            return new AmountResult(0, 0);
        }
        var vatPercentage = vatAmount / value;
        return new AmountResult(
            Math.Round(value, _amountDecimalPlaces),
            Math.Round(vatPercentage, _multiplierDecimalPlaces));
    }
}