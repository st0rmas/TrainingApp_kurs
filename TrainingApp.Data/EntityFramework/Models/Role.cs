namespace TrainingApp.Data.EntityFramework.Models;

public record Role
{
	public required int Id { get; init; }
	public required string Name { get; init; }

	public ICollection<Profile> Profiles { get; set; }
}
