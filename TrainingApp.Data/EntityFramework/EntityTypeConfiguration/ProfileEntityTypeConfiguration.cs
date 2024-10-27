using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingApp.Data.EntityFramework.Models;

namespace TrainingApp.Data.EntityFramework.EntityTypeConfiguration;

public sealed class ProfileEntityTypeConfiguration : IEntityTypeConfiguration<Profile>
{
	public void Configure(EntityTypeBuilder<Profile> builder)
	{
		builder
			.HasMany(user => user.Roles)
			.WithMany(role => role.Profiles)
			.UsingEntity(entity => entity.ToTable("ProfileRoles"));

		builder.HasIndex(user => user.Login).IsUnique();
	}
}