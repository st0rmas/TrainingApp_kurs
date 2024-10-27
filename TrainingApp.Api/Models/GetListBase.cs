using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.Application.Models;

namespace TrainingApp.Models;

public abstract record GetListBase
{
	[FromQuery(Name = "take")]
	public int? Take { get; init; }

	[FromQuery(Name = "skip")]
	public int? Skip { get; init; }

	[FromQuery(Name = "query")]
	public string? Query { get; init; }

	[FromQuery(Name = "sortingDirection")]
	public SortingDirection? SortingDirection { get; init; }

	public class Validator : AbstractValidator<GetListBase>
	{
		public Validator()
		{
			RuleFor(x => x.Skip).GreaterThanOrEqualTo(0).When(x => x.Skip.HasValue);
			RuleFor(x => x.Take).GreaterThanOrEqualTo(0).When(x => x.Take.HasValue);
		}
	}
}