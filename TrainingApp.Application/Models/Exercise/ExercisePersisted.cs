namespace TrainingApp.Application.Models.Exercise;

public sealed record ExercisePersisted
{
	public Guid Id { get; init; }

	public required string Name { get; init; }

	public required string Description { get; init; }
}