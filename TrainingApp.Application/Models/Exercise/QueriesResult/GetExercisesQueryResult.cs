namespace TrainingApp.Application.Models.Exercise.QueriesResult;

public sealed record GetExercisesQueryResult
{
	public required IReadOnlyCollection<ExercisePersisted> Exercises { get; init; }
	
	public required int TotalCount { get; init; }
}