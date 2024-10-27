namespace TrainingApp.Application.Models.Exercise.Queries;

public record CreateExerciseCommand
{
	public required CreateExerciseDto CreateExerciseDto { get; init; }
}