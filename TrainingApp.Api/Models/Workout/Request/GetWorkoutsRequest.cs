using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.Application.Models.Workout;

namespace TrainingApp.Models.Workout.Request;

public record GetWorkoutsRequest : GetListBase
{
	[FromQuery]
	public WorkoutSortField? SortField { get; init; }

	public class Validator : AbstractValidator<GetWorkoutsRequest>
	{
		public Validator()
		{
			RuleFor(x => x.Skip).GreaterThanOrEqualTo(0).When(x => x.Skip.HasValue);
			RuleFor(x => x.Take).GreaterThanOrEqualTo(0).When(x => x.Take.HasValue);
		}
	}
}