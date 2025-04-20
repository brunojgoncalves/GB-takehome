using AmountCalculator.API.Dtos;
using AmountCalculator.API.Extensions;
using AmountCalculator.API.Validators;
using System.Threading.Tasks;

namespace AmountCalculator.API.Tests.Validators;

public class CalculationRequestValidatorTests
{
    private readonly CalculationRequestValidator _validator = new();

    [Fact]
    public async Task Should_Have_Error_When_Percentage_Is_Zero()
    {
        // Arrange
        var request = new CalculationRequest
        {
            Percentage = 0,
            NetAmount = 100
        };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("Percentage must be 10, 13 or 20", result.Errors.Single().ErrorMessage);
    }

    [Fact]
    public async Task Should_Have_Error_When_No_Amounts_Are_Provided()
    {
        // Arrange
        var request = new CalculationRequest
        {
            Percentage = 10
        };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("Only one of the 'NetAmount', 'VATAmount', 'GrossAmount' must be provided", result.Errors.Single().ErrorMessage);
    }

    [Fact]
    public async Task Should_Have_Error_When_Multiple_Amounts_Are_Provided()
    {
        // Arrange
        var request = new CalculationRequest
        {
            Percentage = 10,
            NetAmount = 100,
            VATAmount = 20
        };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("Only one of the 'NetAmount', 'VATAmount', 'GrossAmount' must be provided", result.Errors.Single().ErrorMessage);
    }

    [Fact]
    public async Task Should_Not_Have_Error_When_Only_NetAmount_Is_Provided()
    {
        // Arrange
        var request = new CalculationRequest
        {
            Percentage = 10,
            NetAmount = 100
        };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Should_Not_Have_Error_When_Only_VATAmount_Is_Provided()
    {
        // Arrange
        var request = new CalculationRequest
        {
            Percentage = 10,
            VATAmount = 20
        };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Should_Not_Have_Error_When_Only_GrossAmount_Is_Provided()
    {
        // Arrange
        var request = new CalculationRequest
        {
            Percentage = 10,
            GrossAmount = 120
        };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        Assert.True(result.IsValid);
    }
}