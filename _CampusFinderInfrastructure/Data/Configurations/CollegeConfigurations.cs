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
	public class CollegeConfigurations : IEntityTypeConfiguration<College>
	{
		public void Configure(EntityTypeBuilder<College> builder)
		{
			builder.ToTable("Colleges");

			builder.HasKey(c => c.CollegeID);

			builder.Property(c => c.Description)
				.HasColumnType("nvarchar(max)");

			builder.HasOne(c => c.University)
				.WithMany(u => u.Colleges)
				.HasForeignKey(c => c.UniversityID)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(c => c.Majors)
				.WithOne(m => m.College)
				.HasForeignKey(m => m.CollegeID)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(c => c.CollegeDiplomas)
				.WithOne(cd => cd.College)
				.HasForeignKey(cd => cd.CollegeID)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(c => c.CollegeEnglishTests)
				.WithOne(cet => cet.College)
				.HasForeignKey(cet => cet.CollegeID)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}

}
