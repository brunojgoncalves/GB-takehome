using AmountCalculator.API.Calculators;
using AmountCalculator.API.Dtos;

namespace AmountCalculator.API.Tests.Calculators;

public class CalculatorFromVATAmountTests
{
    private readonly CalculatorFromVATAmount _sut;

    public CalculatorFromVATAmountTests()
    {
        _sut = new CalculatorFromVATAmount();
    }

    [Fact]
    public void Calculate_ValidVATAmountAndPercentage_ReturnsCorrectResponse()
    {
        // Arrange
        var request = new CalculationRequest
        {
            VATAmount = 20m,
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
    public void Calculate_NullVATAmount_ThrowsArgumentNullException()
    {
        // Arrange
        var request = new CalculationRequest
        {
            VATAmount = null,
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
            VATAmount = 0,
            Percentage = 0
        };

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Calculate(request));
    }

    [Fact]
    public void Calculate_VATPercentageIs100_ThrowsException()
    {
        // Arrange
        var request = new CalculationRequest
        {
            VATAmount = 50,
            Percentage = 100
        };

        // Act
        var result = _sut.Calculate(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(50, result.VATAmount);
        Assert.Equal(50, result.NetAmount.Value);
        Assert.Equal(1, result.NetAmount.MultiplierToVAT);
        Assert.Equal(50, result.NetAmount.Value);
        Assert.Equal(1, result.NetAmount.MultiplierToVAT);
    }
}