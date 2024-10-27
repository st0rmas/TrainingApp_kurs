using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.Application.Models.Exercise;

namespace TrainingApp.Models.Exercise.Request;

public sealed record GetExercisesRequest : GetListBase
{
	[FromQuery]
	public ExerciseSortField? SortField { get; init; }

	public class Validator : AbstractValidator<GetExercisesRequest>
	{
		public Validator()
		{
			Include(new GetListBase.Validator()); 
		}
	}
}