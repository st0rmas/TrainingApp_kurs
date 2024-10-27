using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OneOf;
using TrainingApp.Application.Extensions;
using TrainingApp.Application.Models;
using TrainingApp.Application.Models.Errors;
using TrainingApp.Application.Models.Exercise;
using TrainingApp.Application.Models.Workout;
using TrainingApp.Application.Models.Workout.Queries;
using TrainingApp.Application.Models.Workout.QueryResult;
using TrainingApp.Data.EntityFramework;
using TrainingApp.Data.EntityFramework.Models;

namespace TrainingApp.Application.Services.Impl;

public class WorkoutService : IWorkoutService
{
	private readonly ApplicationDbContext _db;

	public WorkoutService(ApplicationDbContext db)
	{
		_db = db ?? throw new ArgumentNullException(nameof(db));
	}

	public async Task<GetWorkoutsQueryResult> GetWorkouts(GetWorkoutsQuery query, CancellationToken cancellationToken)
	{
		var workoutsQuery = _db.Workouts.AsQueryable();

		// Filter

		if (!string.IsNullOrEmpty(query.Query))
		{
			workoutsQuery = workoutsQuery
				.Where(x => EF.Functions.ILike(x.Title, $"%{query.Query}%"));
		}

		var count = await workoutsQuery.CountAsync(cancellationToken);

		if (count == 0)
		{
			return new GetWorkoutsQueryResult()
			{
				TotalCount = 0,
				Workouts = []
			};
		}

		// Sorting and pagination

		var sorting = query.SortField == null
			? new Sorting<WorkoutSortField>(WorkoutSortField.Id, query.SortingDirection)
			: new Sorting<WorkoutSortField>(query.SortField, query.SortingDirection);

		workoutsQuery = workoutsQuery
			.Sort(sorting, TranslateEnumToField)
			.Page(query.Pagination); 

		var workouts = await workoutsQuery.Select(x => new WorkoutPersisted()
		{
			Id = x.Id,
			Title = x.Title,
			Description = x.Description,
			Duration = x.Duration
		}).ToListAsync(cancellationToken);

		return new GetWorkoutsQueryResult()
		{
			Workouts = workouts,
			TotalCount = count
		};
	}

	public async Task<OneOf<WorkoutPersisted, NotFoundById>> GetWorkoutById(GetWorkoutByIdQuery query, CancellationToken cancellationToken)
	{
		var workout = await _db.Workouts
			.Include(workout => workout.WorkoutExercises)
			.ThenInclude(workoutExercises => workoutExercises.Exercise)
			.SingleOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

		if (workout is null)
		{
			return new NotFoundById()
			{
				Id = query.Id
			};
		}

		var workoutExercises = workout.WorkoutExercises.Select(x => new ExercisePersisted()
		{
			Id = x.Exercise.Id,
			Name = x.Exercise.Name,
			Description = x.Exercise.Description
		}).ToArray();

		return new WorkoutPersisted()
		{
			Id = workout.Id,
			Title = workout.Title,
			Description = workout.Description,
			Duration = workout.Duration,
			Exercises = workoutExercises
		};
	}

	private static Expression<Func<Workout, object?>> TranslateEnumToField(WorkoutSortField sortingField) =>
		sortingField switch
		{
			WorkoutSortField.Id => c => c.Id,
			WorkoutSortField.Title => c => c.Title,
			_ => throw new ArgumentOutOfRangeException(nameof(sortingField), sortingField,
				$"Неизвестное поле сортировки {sortingField}")
		};
}