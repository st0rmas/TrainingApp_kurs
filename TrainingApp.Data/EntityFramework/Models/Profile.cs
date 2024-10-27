namespace TrainingApp.Data.EntityFramework.Models;

public record class Profile
{
	public Guid Id { get; init; }

	public required string Name { get; init; }

	public required decimal Weight { get; init; }

	public required int Height { get; init; }

	public required DateOnly Birthday { get; init; }

	public required string Login { get; init; }

	public required string Password { get; init; }

	public required DateTime CreatedAt { get; init; }

	public IEnumerable<Role> Roles { get; set; }
}