using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TrainingApp.Application.Models.Errors;

public record NotFoundById : BusinessError
{
	public required Guid Id { get; init; }
	
	public override string Message => $"Сущность с Id={Id} не найдена";
	
	public override ProblemDetails ToProblemDetails()
	{
		return new ProblemDetails()
		{
			Detail = Message,
			Status = StatusCodes.Status404NotFound,
		};
	}
}