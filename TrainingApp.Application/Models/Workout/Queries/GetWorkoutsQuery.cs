namespace TrainingApp.Application.Models.Workout.Queries;

public sealed record GetWorkoutsQuery : GetListBase
{
	public WorkoutSortField? SortField { get; init; }
}