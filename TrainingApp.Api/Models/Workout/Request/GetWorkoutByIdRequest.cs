using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace TrainingApp.Models.Workout.Request;

public sealed record GetWorkoutByIdRequest
{
	[FromRoute]
	public required Guid Id { get; init; }

	public class Validator : AbstractValidator<GetWorkoutByIdRequest>
	{
		public Validator()
		{
			RuleFor(x => x.Id).NotEmpty();
		}
	}
}