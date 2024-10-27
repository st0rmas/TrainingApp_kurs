namespace TrainingApp.Application.Models.Workout.QueryResult;

public sealed record GetWorkoutsQueryResult
{
	public required IReadOnlyCollection<WorkoutPersisted> Workouts { get; init; }
	
	public required int TotalCount { get; init; }
}