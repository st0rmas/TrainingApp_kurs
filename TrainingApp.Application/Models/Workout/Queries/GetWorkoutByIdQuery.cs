namespace TrainingApp.Application.Models.Workout.Queries;

public sealed record GetWorkoutByIdQuery
{
	public required Guid Id { get; init; }
}