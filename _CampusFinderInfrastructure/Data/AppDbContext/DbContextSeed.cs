using _CampusFinderCore.Entities.UniversityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace _CampusFinderInfrastructure.Data.AppDbContext
{
	public class DbContextSeed
	{
		public static async Task SeedAsync(ApplicationDbContext _dbcontext)
		{
			var universityData = File.ReadAllText("../_CampusFinderInfrastructure/AppDbContext/DataSeed/University.json");
			var universities = JsonSerializer.Deserialize<List<University>>(universityData);
			if (universities.Count > 0)
			{
				if (!(_dbcontext.Universities.Count() > 0))
				{
					foreach (var uni in universities)
					{
						_dbcontext.Set<University>().Add(uni);
					}
					await _dbcontext.SaveChangesAsync();
				}
			}

		}
	}

}
