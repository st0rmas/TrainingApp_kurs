using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using TrainingApp.Data.EntityFramework;
using TrainingApp.Extensions;
using TrainingApp.Filters;

namespace TrainingApp;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		
		builder.Services.AddDbContext<ApplicationDbContext>(
			options => options.UseNpgsql(builder.Configuration["Storage:ConnectionString"]));

		builder.Services.RegisterServices();
		
		builder.Services.AddControllers(x =>
		{
			x.Filters.Add<ValidationActionFilter>();
		});

		builder.Services.AddEndpointsApiExplorer();
		
		// Валидация
		builder.Services.AddScoped<ValidationActionFilter>()
			.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
			.AddFluentValidationAutoValidation();
		
		builder.Services.AddSwaggerGen(x =>
		{
			x.DescribeAllParametersInCamelCase();
			x.SchemaFilter<EnumSchemaFilter>();
		});

		builder.Services.AddProblemDetails();
		
		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseExceptionHandler();
		
		app.UseRouting();

		app.MapControllers();

		app.Run();
	}
}