namespace TrainingApp.Application.Models;

public record Sorting<TField> where TField : struct, Enum
{
	public List<KeyValuePair<TField, SortingDirection>> Sort { get; init; }

	public Sorting(TField? field, SortingDirection? direction) =>
		_ = field is null
			? Sort = new List<KeyValuePair<TField, SortingDirection>>()
			: Sort = new List<KeyValuePair<TField, SortingDirection>> { new(field.Value, direction ?? SortingDirection.Ascending) };
}