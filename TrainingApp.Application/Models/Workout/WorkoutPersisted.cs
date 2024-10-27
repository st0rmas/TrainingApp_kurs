using TrainingApp.Application.Models.Exercise;

namespace TrainingApp.Application.Models.Workout;

public sealed record WorkoutPersisted
{
	public Guid Id { get; init; }

	public required string Title { get; init; }

	public required string Description { get; init; }

	public required int Duration { get; init; }
	
	public IReadOnlyCollection<ExercisePersisted>? Exercises { get; init; }
}