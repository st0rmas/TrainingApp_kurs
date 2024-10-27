using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TrainingApp.Application.Models.Errors;

public record NameTaken : BusinessError
{
	public required string Name { get; init; }
	public override string Message => $"Сущность с названием ({Name}) уже существует";
	
	public override ProblemDetails ToProblemDetails()
	{
		return new ProblemDetails()
		{
			Detail = Message,
			Status = StatusCodes.Status400BadRequest
		};
	}
}