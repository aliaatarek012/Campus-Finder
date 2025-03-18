using _CampusFinderCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderInfrastructure.Configurations
{
	public class English_TestConfigurations : IEntityTypeConfiguration<English_Test>
	{
		public void Configure(EntityTypeBuilder<English_Test> builder)
		{
			builder.HasKey(e => e.TestID);

			builder.HasMany(e => e.CollegeEnglishTests)
				.WithOne(c => c.English_Test)
				.HasForeignKey(c => c.TestID)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
