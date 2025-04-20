namespace AmountCalculator.API.Extensions;

public struct Percentage
{
    public decimal Value { get; private set; }

    public Percentage(int value)
        : this()
    {
        Value = value;
    }

    public Percentage(decimal value)
        : this()
    {
        Value = value;
    }

    public static implicit operator Percentage(decimal value)
    {
        return new Percentage(value);
    }
}