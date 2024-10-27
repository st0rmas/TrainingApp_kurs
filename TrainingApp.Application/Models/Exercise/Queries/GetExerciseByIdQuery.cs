namespace TrainingApp.Application.Models.Exercise.Queries;

public record GetExerciseByIdQuery
{
	public required Guid Id { get; init; }
}