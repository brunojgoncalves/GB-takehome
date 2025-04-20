using AmountCalculator.API.Calculators;
using AmountCalculator.API.Dtos;

namespace AmountCalculator.API.Tests.Calculators;

public class CalculatorFromGrossAmountTests
{
    private readonly CalculatorFromGrossAmount _sut;

    public CalculatorFromGrossAmountTests()
    {
        _sut = new CalculatorFromGrossAmount();
    }

    [Fact]
    public void Calculate_ValidGrossAmountAndPercentage_ReturnsCorrectResponse()
    {
        // Arrange
        var request = new CalculationRequest
        {
            GrossAmount = 120m,
            Percentage = 20m
        };

        // Act
        var response = _sut.Calculate(request);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(100m, response.NetAmount.Value);
        Assert.Equal(20m, response.VATAmount);
        Assert.Equal(120m, response.GrossAmount.Value);
    }

    [Fact]
    public void Calculate_NullGrossAmount_ThrowsArgumentNullException()
    {
        // Arrange
        var request = new CalculationRequest
        {
            GrossAmount = null,
            Percentage = 20m
        };

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _sut.Calculate(request));
    }

    [Fact]
    public void Calculate_VATIsZero_ThrowsException()
    {
        // Arrange
        var request = new CalculationRequest
        {
            GrossAmount = 0,
            Percentage = 0
        };

        // Act
        var result = _sut.Calculate(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(0, result.VATAmount);
        Assert.Equal(0, result.NetAmount.Value);
        Assert.Equal(0, result.NetAmount.MultiplierToVAT);
        Assert.Equal(0, result.GrossAmount.Value);
        Assert.Equal(0, result.GrossAmount.MultiplierToVAT);
    }

    [Fact]
    public void Calculate_VATPercentageIs100_ThrowsException()
    {
        // Arrange
        var request = new CalculationRequest
        {
            GrossAmount = 50,
            Percentage = 100
        };

        // Act
        var result = _sut.Calculate(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(25, result.VATAmount);
        Assert.Equal(25, result.NetAmount.Value);
        Assert.Equal(1, result.NetAmount.MultiplierToVAT);
        Assert.Equal(50, result.GrossAmount.Value);
        Assert.Equal(0.5m, result.GrossAmount.MultiplierToVAT);
    }
}