namespace TrainingApp.Application.Models.Exercise;

public sealed record CreateExerciseDto
{
	public required string Name { get; init; }

	public required string Description { get; init; }
}