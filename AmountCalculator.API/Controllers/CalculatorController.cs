using AmountCalculator.API.Calculators;
using AmountCalculator.API.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AmountCalculator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly IValidator<CalculationRequest> _calculationRequestValidator;
    private readonly ICalculationStrategyProvider _calculatorStrategyProvider;

    public CalculatorController(IValidator<CalculationRequest> calculationRequestValidator,
        ICalculationStrategyProvider calculatorStrategyProvider)
    {
        ArgumentNullException.ThrowIfNull(_calculationRequestValidator = calculationRequestValidator);
        ArgumentNullException.ThrowIfNull(_calculatorStrategyProvider = calculatorStrategyProvider);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CalculationResponse), 200)]
    public async Task<IActionResult> Calculate([FromBody] CalculationRequest calculationRequest, CancellationToken cancellationToken)
    {
        var validationResult = await _calculationRequestValidator.ValidateAsync(calculationRequest, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(a => a.ErrorMessage));
        }

        var calculatorStrategy = _calculatorStrategyProvider
            .GetCalculatorStrategy(calculationRequest);

        return Ok(calculatorStrategy.Calculate(calculationRequest));
    }
}