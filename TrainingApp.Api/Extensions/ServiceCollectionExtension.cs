using TrainingApp.Application.Services;
using TrainingApp.Application.Services.Impl;

namespace TrainingApp.Extensions;

public static class ServiceCollectionExtension
{
	public static IServiceCollection RegisterServices(this IServiceCollection services)
	{
		services.AddScoped<IExerciseService, ExerciseService>();
		services.AddScoped<IWorkoutService, WorkoutService>();

		return services;
	}
}