using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TrainingApp.Data.EntityFramework.Models;

namespace TrainingApp.Data.EntityFramework;

public class ApplicationDbContext : DbContext
{
	public DbSet<Profile> Profiles { get; set; }
	public DbSet<Role> Roles { get; set; }
	public DbSet<TrainingType> TrainingTypes { get; set; }
	public DbSet<Exercise> Exercises { get; set; }
	public DbSet<Workout> Workouts { get; set; }
	public DbSet<WorkoutExercises> WorkoutExercises { get; set; }
	
	public ApplicationDbContext(DbContextOptions options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}
