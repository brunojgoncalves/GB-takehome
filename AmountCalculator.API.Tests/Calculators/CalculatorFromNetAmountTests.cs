using AmountCalculator.API.Calculators;
using AmountCalculator.API.Dtos;

namespace AmountCalculator.API.Tests.Calculators;

public class CalculatorFromNetAmountTests
{
    private readonly CalculatorFromNetAmount _sut;

    public CalculatorFromNetAmountTests()
    {
        _sut = new CalculatorFromNetAmount();
    }

    [Fact]
    public void Calculate_ValidNetAmountAndPercentage_ReturnsCorrectResponse()
    {
        // Arrange
        var request = new CalculationRequest
        {
            NetAmount = 100m,
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
    public void Calculate_NullNetAmount_ThrowsArgumentNullException()
    {
        // Arrange
        var request = new CalculationRequest
        {
            NetAmount = null,
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
            NetAmount = 0,
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
            NetAmount = 50,
            Percentage = 100
        };

        // Act
        var result = _sut.Calculate(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(50, result.VATAmount);
        Assert.Equal(50, result.NetAmount.Value);
        Assert.Equal(1, result.NetAmount.MultiplierToVAT);
        Assert.Equal(100, result.GrossAmount.Value);
        Assert.Equal(0.5m, result.GrossAmount.MultiplierToVAT);
    }
}