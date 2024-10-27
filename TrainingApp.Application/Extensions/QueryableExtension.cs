using System.Linq.Expressions;
using TrainingApp.Application.Models;

namespace TrainingApp.Application.Extensions;

public static class QueryableExtension
{
	public static IQueryable<TQuery> Sort<TQuery, TSortingField>(
		this IQueryable<TQuery> query,
		Sorting<TSortingField>? sorting,
		Func<TSortingField, Expression<Func<TQuery, object?>>> keySelector)
		where TSortingField : struct, Enum
	{
		if (sorting == null || !sorting.Sort.Any())
		{
			return query;
		}

		var (field1, direction1) = sorting.Sort.First();

		var orderedQuery = direction1 switch
		{
			SortingDirection.Ascending => query.OrderBy(keySelector(field1)),
			SortingDirection.Descending => query.OrderByDescending(keySelector(field1)),
			_ => throw new NotSupportedException($"{direction1} не поддерживается")
		};

		foreach (var (field, direction) in sorting.Sort.Skip(1))
		{
			orderedQuery = direction switch
			{
				SortingDirection.Ascending => orderedQuery.ThenBy(keySelector(field)),
				SortingDirection.Descending => orderedQuery.ThenByDescending(keySelector(field)),
				_ => throw new NotSupportedException($"{direction} не поддерживается")
			};
		}

		return orderedQuery;
	}

	/// <summary>
	/// Пагинация
	/// </summary>
	public static IQueryable<TQuery> Page<TQuery>(this IQueryable<TQuery> query, Pagination? pagination)
	{
		ArgumentNullException.ThrowIfNull(pagination);

		query = query
			.Skip(pagination.Skip ?? 0)
			.Take(pagination.Take ?? 100);

		return query;
	}
}