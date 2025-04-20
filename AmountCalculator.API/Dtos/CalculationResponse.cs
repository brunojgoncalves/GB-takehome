namespace AmountCalculator.API.Dtos;

public record CalculationResponse
{
    public required AmountResult NetAmount { get; set; }
    public required AmountResult GrossAmount { get; set; }
    public required decimal VATAmount { get; set; }
}