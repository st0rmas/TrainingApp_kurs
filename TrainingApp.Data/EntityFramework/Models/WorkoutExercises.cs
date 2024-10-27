namespace TrainingApp.Data.EntityFramework.Models;

public class WorkoutExercises
{
	public required Guid WorkoutId { get; init; }
	
	public required Workout Workout { get; init; }

	public required Guid ExerciseId { get; init; }

	public required Exercise Exercise { get; init; }

	public required int Sets { get; init; }

	public required int Reps { get; init; }
}