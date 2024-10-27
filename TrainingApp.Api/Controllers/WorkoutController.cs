using Microsoft.AspNetCore.Mvc;
using TrainingApp.Application.Models;
using TrainingApp.Application.Models.Workout.Queries;
using TrainingApp.Application.Services;
using TrainingApp.Models.Workout.Request;
using TrainingApp.Models.Workout.Response;

namespace TrainingApp.Controllers;

[ApiController]
[Route("workouts")]
public class WorkoutController : ControllerBase
{
	private readonly IWorkoutService _workoutService;

	public WorkoutController(IWorkoutService workoutService)
	{
		_workoutService = workoutService ?? throw new ArgumentNullException(nameof(workoutService));
	}

	[HttpGet]
	public async Task<IActionResult> GetWorkouts(GetWorkoutsRequest request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);

		var getWorkoutsQuery = new GetWorkoutsQuery()
		{
			Query = request.Query,
			SortField = request.SortField,
			SortingDirection = request.SortingDirection,
			Pagination = new Pagination()
			{
				Take = request.Take,
				Skip = request.Skip
			}
		};

		var response = await _workoutService.GetWorkouts(getWorkoutsQuery, cancellationToken);

		var getWorkoutsResponse = new GetWorkoutsResponse()
		{
			Workouts = response.Workouts,
			TotalCount = response.TotalCount
		};

		return Ok(getWorkoutsResponse);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetWorkoutById(GetWorkoutByIdRequest request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);

		var getWorkoutByIdQuery = new GetWorkoutByIdQuery()
		{
			Id = request.Id
		};

		var response = await _workoutService.GetWorkoutById(getWorkoutByIdQuery, cancellationToken);

		return response.Match<IActionResult>(
			workout => Ok(workout),
			error => NotFound(error.ToProblemDetails())
		);
	}
	
}