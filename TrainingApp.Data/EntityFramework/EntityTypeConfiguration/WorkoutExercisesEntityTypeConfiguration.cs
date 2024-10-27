using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingApp.Data.EntityFramework.Models;

namespace TrainingApp.Data.EntityFramework.EntityTypeConfiguration;

public class WorkoutExercisesEntityTypeConfiguration : IEntityTypeConfiguration<WorkoutExercises>
{
	public void Configure(EntityTypeBuilder<WorkoutExercises> builder)
	{
		builder.HasKey(we => new { we.WorkoutId, we.ExerciseId });		

		builder
			.HasOne(we => we.Workout)
			.WithMany(w => w.WorkoutExercises)
			.HasForeignKey(we => we.WorkoutId);
		
		builder
			.HasOne(we => we.Exercise)
			.WithMany(e => e.WorkoutExercises)
			.HasForeignKey(we => we.ExerciseId);
	}
}