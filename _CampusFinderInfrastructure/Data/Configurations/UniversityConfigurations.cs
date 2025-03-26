using _CampusFinderCore.Entities.UniversityEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderInfrastructure.Data.Configurations
{
	public class UniversityConfigurations : IEntityTypeConfiguration<University>
	{
		public void Configure(EntityTypeBuilder<University> builder)
		{
			builder.ToTable("Universities");

			builder.HasKey(u => u.UniversityID);

			builder.Property(u => u.Name).IsRequired().HasMaxLength(100);

			builder.Property(u => u.Description)
				   .HasColumnType("nvarchar(max)");

			builder.Property(u => u.RequiredDocuments)
				   .HasColumnType("nvarchar(max)");

			builder.HasIndex(u => u.Rank);
			builder.HasIndex(u => u.Location);
			builder.HasIndex(u => u.Name);

			builder.HasMany(u => u.Colleges)
				.WithOne(c => c.University)
				.HasForeignKey(c => c.UniversityID)
				.OnDelete(DeleteBehavior.Cascade);

		}
	}
}
