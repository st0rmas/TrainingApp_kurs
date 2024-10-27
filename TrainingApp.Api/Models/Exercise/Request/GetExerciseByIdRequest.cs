using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace TrainingApp.Models.Exercise.Request;

public record GetExerciseByIdRequest
{
	[FromRoute(Name = "id")]
	public required Guid Id { get; init; }

	public class Validator : AbstractValidator<GetExerciseByIdRequest>
	{
		public Validator()
		{
			RuleFor(x => x.Id).NotEmpty();
		}
	}
}