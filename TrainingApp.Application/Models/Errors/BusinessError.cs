using Microsoft.AspNetCore.Mvc;

namespace TrainingApp.Application.Models.Errors;

public abstract record BusinessError
{
	public abstract string Message { get; }

	public abstract ProblemDetails ToProblemDetails();
}