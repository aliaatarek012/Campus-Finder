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
	public class College_EnglishConfigurations : IEntityTypeConfiguration<College_English>
	{
		public void Configure(EntityTypeBuilder<College_English> builder)
		{
			builder.HasKey(ce => new { ce.CollegeID, ce.TestID });

			builder.HasOne(ce => ce.College)
				.WithMany(c => c.CollegeEnglishTests)
				.HasForeignKey(ce => ce.CollegeID)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(ce => ce.English_Test)
				.WithMany(et => et.CollegeEnglishTests)
				.HasForeignKey(ce => ce.TestID)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasIndex(ce => ce.CollegeID);
			builder.HasIndex(ce => ce.TestID);
		}
	}

}
