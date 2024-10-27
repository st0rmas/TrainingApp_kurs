using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingApp.Data.EntityFramework.Models;

namespace TrainingApp.Data.EntityFramework.EntityTypeConfiguration;

public sealed class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
{
	public void Configure(EntityTypeBuilder<Role> builder)
	{
		builder.HasData(new List<Role>()
		{
			new ()
			{
				Id = (int)RoleEnum.Admin,
				Name = RoleEnum.Admin.ToString()
			},
			new ()
			{
				Id = (int)RoleEnum.User,
				Name = RoleEnum.User.ToString()
			}
		});

		builder.HasIndex(role => role.Name).IsUnique();
	}
}