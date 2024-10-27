namespace TrainingApp.Data.EntityFramework.Models;

public class Training
{
	public Guid Id { get; init; }
	
	public required TrainingType TrainingType { get; init; }
	
	public required int Distance { get; init; }
	
	public required int Minutes { get; init; }
	
	public required int Calories { get; init; }
	
	public required Guid ProfileId { get; init; }
	
	public required Profile Profile { get; init; }
	
	public required DateTime CreatedAt { get; init; }
}