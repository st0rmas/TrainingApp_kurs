using TrainingApp.Application.Models.Workout;

namespace TrainingApp.Models.Workout.Response;

public record GetWorkoutsResponse
{
	public required IReadOnlyCollection<WorkoutPersisted> Workouts { get; init; }
	
	public required int TotalCount { get; init; }
}