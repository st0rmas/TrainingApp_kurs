using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingApp.Data.EntityFramework.Models;

namespace TrainingApp.Data.EntityFramework.EntityTypeConfiguration;

public class TrainingTypeEntityTypeConfiguration : IEntityTypeConfiguration<TrainingType>
{
	public void Configure(EntityTypeBuilder<TrainingType> builder)
	{
		builder.HasData(new List<TrainingType>()
		{
			new ()
			{
				Id = (int)TrainingTypeEnum.RegularRunning,
				Name = "Базовый бег"
			},
			new ()
			{
				Id = (int)TrainingTypeEnum.TrackRunning,
				Name = "Бег по стадиону"
			},
			new ()
			{
				Id = (int)TrainingTypeEnum.Walking,
				Name = "Ходьба"
			},
			new ()
			{
				Id = (int)TrainingTypeEnum.RecoveryRunning,
				Name = "Восстановительный бег"
			},
			new ()
			{
				Id = (int)TrainingTypeEnum.TrialRunning,
				Name = "Бег по пересеченной местности"
			}
		});

		builder.HasIndex(role => role.Name).IsUnique();
	}
}