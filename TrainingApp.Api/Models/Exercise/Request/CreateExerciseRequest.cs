using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.Application.Models.Exercise;

namespace TrainingApp.Models.Exercise.Request;

public class CreateExerciseRequest
{
	[FromBody]
	public required CreateExerciseDto CreateExerciseDto { get; init; }

	public class Validator : AbstractValidator<CreateExerciseRequest>
	{
		public Validator()
		{
			RuleFor(x => x.CreateExerciseDto.Name).NotEmpty();
			RuleFor(x => x.CreateExerciseDto.Description).NotEmpty();
		}
	}
}