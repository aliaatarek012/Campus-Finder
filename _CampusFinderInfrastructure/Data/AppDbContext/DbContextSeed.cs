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
			var universityData = File.ReadAllText("../_CampusFinderInfrastructure/Data/AppDbContext/DataSeed/University.json");
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

			var DiplomaData = File.ReadAllText("../_CampusFinderInfrastructure/Data/AppDbContext/DataSeed/Diploma.json");
			var diplomas = JsonSerializer.Deserialize<List<Diploma>>(DiplomaData);
			if (diplomas.Count > 0)
			{
				if (!(_dbcontext.Diplomas.Count() > 0))
				{
					foreach (var diploma in diplomas)
					{
						_dbcontext.Set<Diploma>().Add(diploma);
					}
					await _dbcontext.SaveChangesAsync();
				}
			}


			var EnglishData = File.ReadAllText("../_CampusFinderInfrastructure/Data/AppDbContext/DataSeed/English_Test.json");
			var tests = JsonSerializer.Deserialize<List<English_Test>>(EnglishData);	
			if(tests.Count > 0)
				{
				if (!(_dbcontext.EnglishTests.Count() > 0))
				{
					foreach (var test in tests)
					{
						_dbcontext.Set<English_Test>().Add(test);
					}
					await _dbcontext.SaveChangesAsync();
				}
			}

			var collegesData = File.ReadAllText("../_CampusFinderInfrastructure/Data/AppDbContext/DataSeed/College.json");
			var colleges = JsonSerializer.Deserialize<List<College>>(collegesData);
			if (colleges.Count > 0)
			{
				if (!(_dbcontext.Colleges.Count() > 0))
				{
					foreach (var college in colleges)
					{
						_dbcontext.Set<College>().Add(college);
					}
					await _dbcontext.SaveChangesAsync();
				}
			}

            var majorsData = File.ReadAllText("../_CampusFinderInfrastructure/Data/AppDbContext/DataSeed/Major.json");
            var majors = JsonSerializer.Deserialize<List<Major>>(majorsData);
            if (majors.Count > 0)
            {
                if (!(_dbcontext.Majors.Count() > 0))
                {
                    foreach (var major in majors)
                    {
                        _dbcontext.Set<Major>().Add(major);
                        Console.WriteLine($"Is Entity Tracked: {_dbcontext.Entry(major).State}");
                    }
                    await _dbcontext.SaveChangesAsync();
                    
                }
            }
        }

	}

}
