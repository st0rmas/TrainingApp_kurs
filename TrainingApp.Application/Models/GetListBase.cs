namespace TrainingApp.Application.Models;

public abstract record GetListBase
{
	public string? Query { get; init; }

	public Pagination? Pagination { get; init; }

	public SortingDirection? SortingDirection { get; init; }
}