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
	public class MajorConfigurations : IEntityTypeConfiguration<Major>
	{
		public void Configure(EntityTypeBuilder<Major> builder)
		{
			builder.ToTable("Majors");

			builder.HasKey(m => m.MajorID);
			builder.HasOne(m => m.College)
				.WithMany(c => c.Majors)
				.HasForeignKey(m => m.CollegeID)
				.OnDelete(DeleteBehavior.Cascade);


            builder.Property(m => m.Description)
				.HasColumnType("nvarchar(max)");
		}
	}

}
