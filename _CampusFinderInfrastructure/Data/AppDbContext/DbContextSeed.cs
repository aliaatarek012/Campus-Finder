using _CampusFinderCore.Entities.UniversityEntities;
using Microsoft.EntityFrameworkCore;
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

                if (!(_dbcontext.Majors.Any()))
                {
                    // Open database connection if not already open
                    var connection = _dbcontext.Database.GetDbConnection();
                    if (connection.State != System.Data.ConnectionState.Open)
                        await connection.OpenAsync();

                    using var transaction = await _dbcontext.Database.BeginTransactionAsync();

                    try
                    {
                        // Turn ON identity insert
                        await _dbcontext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Majors ON");

                        foreach (var major in majors)
                        {
                            _dbcontext.Majors.Add(major);
                        }

                        await _dbcontext.SaveChangesAsync();

                        // Turn OFF identity insert
                        await _dbcontext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Majors OFF");

                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw; // or log the error
                    }
                }
            }
			var collegeEnglishData = File.ReadAllText("../_CampusFinderInfrastructure/Data/AppDbContext/DataSeed/College-English.json");
			var collegeEnglish = JsonSerializer.Deserialize<List<College_English>>(collegeEnglishData);
			if (collegeEnglish.Count > 0)
			{
				if (!(_dbcontext.CollegeEnglishTests.Count() > 0))
				{
					foreach (var college in collegeEnglish)
					{
						_dbcontext.Set<College_English>().Add(college);
					}
				}
				await _dbcontext.SaveChangesAsync();
			}

			var CollegeDiplomaData = File.ReadAllText("../_CampusFinderInfrastructure/Data/AppDbContext/DataSeed/College_Diploma.json");
			var collegeDiplomas = JsonSerializer.Deserialize<List<College_Diploma>>(CollegeDiplomaData);
			if (collegeDiplomas.Count > 0)
            {
                if (!(_dbcontext.CollegeDiplomas.Count() > 0))
                {
                    foreach (var college in collegeDiplomas)
                    {
                        _dbcontext.Set<College_Diploma>().Add(college);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }

			var eventsData = File.ReadAllText("../_CampusFinderInfrastructure/Data/AppDbContext/DataSeed/Events.json");
			var events = JsonSerializer.Deserialize<List<Events>>(eventsData);
			if (events.Count > 0)
			{
				if (!(_dbcontext.Events.Count() > 0))
				{
					foreach (var eventItem in events)
					{
						_dbcontext.Set<Events>().Add(eventItem);
					}
					await _dbcontext.SaveChangesAsync();
				}
			}
		}




    }

}
