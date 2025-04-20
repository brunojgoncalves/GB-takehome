namespace AmountCalculator.API.Extensions;

public static class PercentExtension
{
    public static Percentage Percent(this int value)
    {
        return new Percentage(value);
    }

    public static Percentage Percent(this decimal value)
    {
        return new Percentage(value);
    }

    public static decimal Of(this Percentage percentage, decimal value)
    {
        return value * (percentage.Value / 100);
    }

    public static decimal NetAmountFromVATAndVATPercent(this decimal value, Percentage percentage)
    {
        if (value == 0)
        {
            if (percentage.Value == 0)
            {
                return 0;
            }
            throw new InvalidOperationException("The percentage can't be 0 and have a calculated value from it");
        }

        return (value / (percentage.Value / 100));
    }

    public static decimal WithTax(this decimal value, Percentage percentage)
    {
        return value * (1 + percentage.Value / 100);
    }

    public static decimal WithoutTax(this decimal value, Percentage percentage)
    {
        return value / (percentage.Value / 100 + 1);
    }
}