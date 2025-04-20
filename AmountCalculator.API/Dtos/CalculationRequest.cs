namespace AmountCalculator.API.Dtos;

public record CalculationRequest
{
    public decimal? NetAmount { get; set; }
    public decimal? VATAmount { get; set; }
    public decimal? GrossAmount { get; set; }
    public decimal Percentage { get; set; }
}