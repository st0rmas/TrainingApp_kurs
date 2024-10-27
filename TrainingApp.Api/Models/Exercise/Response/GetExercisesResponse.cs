using TrainingApp.Application.Models.Exercise;

namespace TrainingApp.Models.Exercise.Response;

public record GetExercisesResponse
{
	public required IReadOnlyCollection<ExercisePersisted> Exercises { get; init; }
	
	public required int TotalCount { get; init; }
}