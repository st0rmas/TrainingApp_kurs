using System.Linq.Expressions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using TrainingApp.Application.Extensions;
using TrainingApp.Application.Models;
using TrainingApp.Application.Models.Errors;
using TrainingApp.Application.Models.Exercise;
using TrainingApp.Application.Models.Exercise.Queries;
using TrainingApp.Application.Models.Exercise.QueriesResult;
using TrainingApp.Data.EntityFramework;
using TrainingApp.Data.EntityFramework.Models;

namespace TrainingApp.Application.Services.Impl;

public class ExerciseService : IExerciseService
{
	private readonly ApplicationDbContext _db;

	public ExerciseService(ApplicationDbContext db)
	{
		_db = db;
	}

	public async Task<GetExercisesQueryResult> GetExercises(GetExercisesQuery query, CancellationToken cancellationToken)
	{
		var exercisesQuery = _db.Exercises.AsQueryable();
		
		// Filter

		if (!string.IsNullOrEmpty(query.Query))
		{
			exercisesQuery = exercisesQuery
				.Where(x => EF.Functions.ILike(x.Name, $"%{query.Query}%"));
		}
		
		var count = await exercisesQuery.CountAsync(cancellationToken);

		if (count == 0)
		{
			return new GetExercisesQueryResult()
			{
				TotalCount = 0,
				Exercises = []
			};
		}
		
		// Sorting and pagination
		
		var sorting = query.SortField == null
			? new Sorting<ExerciseSortField>(ExerciseSortField.Id, query.SortingDirection)
			: new Sorting<ExerciseSortField>(query.SortField, query.SortingDirection);

		exercisesQuery = exercisesQuery
			.Sort(sorting, TranslateEnumToField)
			.Page(query.Pagination);
		
		var exercisesPersisted = await exercisesQuery.Select(x => new ExercisePersisted()
		{
			Id = x.Id,
			Description = x.Description,
			Name = x.Name 
		}).ToListAsync(cancellationToken);

		return new GetExercisesQueryResult()
		{
			Exercises = exercisesPersisted,
			TotalCount = count
		};
	}

	//todo посмотреть result + oneOf + result pattern в целом
	public async Task<OneOf<ExercisePersisted, NotFoundById>> GetExerciseById(GetExerciseByIdQuery query, CancellationToken cancellationToken)
	{
		var exercise = await _db.Exercises.SingleOrDefaultAsync(exercise => exercise.Id == query.Id, cancellationToken);

		if (exercise is null)
		{
			return new NotFoundById()
			{
				Id = query.Id
			};
		}

		return new ExercisePersisted()
		{
			Id = exercise.Id,
			Description = exercise.Description,
			Name = exercise.Name
		};
	}

	public async Task<OneOf<ExercisePersisted, NameTaken>> CreateExercise(CreateExerciseCommand command, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(command);

		var existingExercise = await _db.Exercises.SingleOrDefaultAsync(x => x.Name == command.CreateExerciseDto.Name, cancellationToken);

		if (existingExercise is not null)
		{
			return new NameTaken()
			{
				Name = command.CreateExerciseDto.Name
			};
		}

		var exercise = new Exercise()
		{
			Name = command.CreateExerciseDto.Name,
			Description = command.CreateExerciseDto.Description
		};

		_db.Exercises.Add(exercise);
		await _db.SaveChangesAsync(cancellationToken);

		return new ExercisePersisted()
		{
			Id = exercise.Id,
			Name = exercise.Name,
			Description = exercise.Description
		};
	}

	private static Expression<Func<Exercise, object?>> TranslateEnumToField(ExerciseSortField sortingField) =>
		sortingField switch
		{
			ExerciseSortField.Id => c => c.Id,
			ExerciseSortField.Name => c => c.Name,
			_ => throw new ArgumentOutOfRangeException(nameof(sortingField), sortingField,
				$"Неизвестное поле сортировки {sortingField}")
		};
}