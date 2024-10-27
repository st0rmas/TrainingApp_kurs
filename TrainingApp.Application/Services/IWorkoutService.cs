using OneOf;
using TrainingApp.Application.Models.Errors;
using TrainingApp.Application.Models.Workout;
using TrainingApp.Application.Models.Workout.Queries;
using TrainingApp.Application.Models.Workout.QueryResult;

namespace TrainingApp.Application.Services;

public interface IWorkoutService
{
	public Task<GetWorkoutsQueryResult> GetWorkouts(GetWorkoutsQuery query, CancellationToken cancellationToken);
	
	public Task<OneOf<WorkoutPersisted, NotFoundById>> GetWorkoutById(GetWorkoutByIdQuery query, CancellationToken cancellationToken);
}