namespace TrainingApp.Data.EntityFramework.Models;

public class Workout
{
	public Guid Id { get; init; }
	
	public required string Title { get; init; }
	
	public required string Description { get; init; }

	public required int Duration { get; init; }
	
	public IEnumerable<WorkoutExercises> WorkoutExercises { get; init; }
}