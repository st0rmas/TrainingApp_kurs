using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingApp.Data.EntityFramework.Models;

namespace TrainingApp.Data.EntityFramework.EntityTypeConfiguration;

public class WorkoutEntityTypeConfiguration : IEntityTypeConfiguration<Workout>
{
	public void Configure(EntityTypeBuilder<Workout> builder)
	{
		
	}
}