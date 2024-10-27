using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TrainingApp.Extensions;

/// <inheritdoc />
// ReSharper disable once ClassNeverInstantiated.Global
public sealed class EnumSchemaFilter : ISchemaFilter
{
	/// <inheritdoc />
	public void Apply(OpenApiSchema model, SchemaFilterContext context)
	{
		if (!context.Type.IsEnum) return;
		model.Type = "string";
		model.Enum.Clear();
		Enum.GetNames(context.Type)
			.ToList()
			.ForEach(n => model.Enum.Add(new OpenApiString(n)));
	}
}