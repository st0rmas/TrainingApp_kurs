namespace TrainingApp.Data.EntityFramework.Models;

public enum TrainingTypeEnum
{
	/// <summary>
	/// Базовый бег
	/// </summary>
	RegularRunning = 1,

	/// <summary>
	/// Бег по стадиону
	/// </summary>
	TrackRunning = 2,

	/// <summary>
	/// Ходьба
	/// </summary>
	Walking = 3,

	/// <summary>
	/// Восстановительный бег
	/// </summary>
	RecoveryRunning = 4,
	
	/// <summary>
	/// Бег по пересеченной местности
	/// </summary>
	TrialRunning = 5
}