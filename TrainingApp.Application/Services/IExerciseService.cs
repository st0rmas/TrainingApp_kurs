using OneOf;
using TrainingApp.Application.Models.Errors;
using TrainingApp.Application.Models.Exercise;
using TrainingApp.Application.Models.Exercise.Queries;
using TrainingApp.Application.Models.Exercise.QueriesResult;

namespace TrainingApp.Application.Services;

public interface IExerciseService
{
	public Task<GetExercisesQueryResult> GetExercises(GetExercisesQuery query, CancellationToken cancellationToken);

	public Task<OneOf<ExercisePersisted, NotFoundById>> GetExerciseById(GetExerciseByIdQuery query, CancellationToken cancellationToken);

	public Task<OneOf<ExercisePersisted, NameTaken>> CreateExercise(CreateExerciseCommand command, CancellationToken cancellationToken);
}