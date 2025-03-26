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
	public class College_DiplomaConfigurations : IEntityTypeConfiguration<College_Diploma>
	{
		public void Configure(EntityTypeBuilder<College_Diploma> builder)
		{
			builder.HasKey(cd => new { cd.CollegeID, cd.DiplomaID });

			builder.Property(cd => cd.Requirments)
				.HasColumnType("nvarchar(max)");

			builder.HasOne(cd => cd.College)
					.WithMany(c => c.CollegeDiplomas)
					.HasForeignKey(cd => cd.CollegeID)
					.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(cd => cd.Diploma)
				   .WithMany(d => d.CollegeDiplomas)
				   .HasForeignKey(cd => cd.DiplomaID)
				   .OnDelete(DeleteBehavior.Cascade);

			// Indexes for faster searching
			builder.HasIndex(cd => cd.CollegeID);
			builder.HasIndex(cd => cd.DiplomaID);
		}
	}

}
