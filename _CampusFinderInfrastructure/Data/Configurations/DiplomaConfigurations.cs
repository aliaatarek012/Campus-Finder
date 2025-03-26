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
	public class DiplomaConfigurations : IEntityTypeConfiguration<Diploma>
	{

		public void Configure(EntityTypeBuilder<Diploma> builder)
		{
			builder.HasKey(d => d.ID);

			builder.HasMany(d => d.CollegeDiplomas)
				.WithOne(cd => cd.Diploma)
				.HasForeignKey(cd => cd.DiplomaID);
		}
	}
}
