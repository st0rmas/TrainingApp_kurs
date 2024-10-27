using Microsoft.AspNetCore.Mvc;
using TrainingApp.Application.Models;
using TrainingApp.Application.Models.Exercise.Queries;
using TrainingApp.Application.Services;
using TrainingApp.Models.Exercise.Request;
using TrainingApp.Models.Exercise.Response;

namespace TrainingApp.Controllers;

[Route("/exercises")]
[ApiController]
public class ExerciseController : ControllerBase
{
	private readonly IExerciseService _exerciseService;

	public ExerciseController(IExerciseService exerciseService)
	{
		_exerciseService = exerciseService;
	}

	[HttpGet]
	public async Task<IActionResult> GetExercises(GetExercisesRequest request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);

		var getExercisesQuery = new GetExercisesQuery()
		{
			Query = request.Query,
			Pagination = new Pagination()
			{
				Skip = request.Skip,
				Take = request.Take
			}
		};

		var response = await _exerciseService.GetExercises(getExercisesQuery, cancellationToken);

		return Ok(new GetExercisesResponse()
		{
			Exercises = response.Exercises,
			TotalCount = response.TotalCount
		});
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetExerciseById(GetExerciseByIdRequest request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);

		var getExercisesByIdQuery = new GetExerciseByIdQuery()
		{
			Id = request.Id
		};

		var response = await _exerciseService.GetExerciseById(getExercisesByIdQuery, cancellationToken);

		return response.Match<IActionResult>(
			exercise => Ok(exercise),
			error => NotFound(error.ToProblemDetails())
		);
	}

	[HttpPost]
	public async Task<IActionResult> CreateExercise(CreateExerciseRequest request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);

		var createExerciseCommand = new CreateExerciseCommand()
		{
			CreateExerciseDto = request.CreateExerciseDto
		};

		var response = await _exerciseService.CreateExercise(createExerciseCommand, cancellationToken);

		return response.Match<IActionResult>(
			exercise => Ok(exercise),
			error => BadRequest(error.ToProblemDetails())
		);
	}
}