using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace TrainingApp.Filters;

/// <summary>
/// Валидирующий фильтр действия.
/// </summary>
public sealed class ValidationActionFilter : IAsyncActionFilter
{
	/// <summary>
	/// Логгер.
	/// </summary>
	private readonly ILogger<ValidationActionFilter> _logger;

	/// <summary>
	/// Инициализирует новый экземпляр класса <see cref="ValidationActionFilter"/>.
	/// </summary>
	/// <param name="logger">
	/// Логгер.
	/// </param>
	/// <exception cref="ArgumentNullException">
	/// <paramref name="logger"/> равен <see langword="null"/>.
	/// </exception>
	public ValidationActionFilter(ILogger<ValidationActionFilter> logger)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	{
		_logger.LogTrace("Вызов {MethodName}(ActionExecutingContext, ActionExecutionDelegate).", nameof(OnActionExecutionAsync));
		ArgumentNullException.ThrowIfNull(context);
		ArgumentNullException.ThrowIfNull(next);

		var request = context
			.ActionArguments
			.SingleOrDefault(actionArgument => actionArgument.Value is not CancellationToken)
			.Value;

		if (request is null)
		{
			_logger.LogDebug("Действие {Action} не содержит запроса, который требует валидации.", context.ActionDescriptor.DisplayName);

			await next();
		}

		else
		{
			_logger.LogDebug("Валидация запроса {Request} действия {Action}.", request, context.ActionDescriptor.DisplayName);

			var requestValidatorType = typeof(IValidator<>).MakeGenericType(request.GetType());
			var requestValidator = (IValidator)context.HttpContext.RequestServices.GetRequiredService(requestValidatorType);
			var validationContext = new ValidationContext<object>(request);
			var requestValidationResult = await requestValidator.ValidateAsync(validationContext, context.HttpContext.RequestAborted);

			if (!requestValidationResult.IsValid)
			{
				_logger.LogWarning("Запрос {Request} действия {Action} не прошел валидацию {ValidationErrors}.", request, context.ActionDescriptor.DisplayName, requestValidationResult.ToDictionary());

				requestValidationResult.AddToModelState(context.ModelState);
				var problemDetailsFactory = context.HttpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();
				var validationProblem = problemDetailsFactory.CreateValidationProblemDetails(context.HttpContext, context.ModelState);
				var actionResult = validationProblem.Status is StatusCodes.Status400BadRequest
					? new BadRequestObjectResult(validationProblem)
					: new ObjectResult(validationProblem)
					{
						StatusCode = validationProblem.Status
					};
				context.Result = actionResult;
			}
			else
			{
				_logger.LogDebug("Запрос {Request} действия {Action} прошёл валидацию.", request, context.ActionDescriptor.DisplayName);

				await next();
			}
		}
	}
}