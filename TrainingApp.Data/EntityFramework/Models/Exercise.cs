namespace TrainingApp.Data.EntityFramework.Models;

public class Exercise
{
	public Guid Id { get; init; }

	public required string Name { get; init; }

	public required string Description { get; init; }
	
	//todo добавить картинку упражнения	
	public IEnumerable<WorkoutExercises> WorkoutExercises { get; init; }
}