namespace TrainingApp.Application.Models.Exercise.Queries;

public record GetExercisesQuery : GetListBase
{
	public ExerciseSortField? SortField { get; init; }
	
	
}