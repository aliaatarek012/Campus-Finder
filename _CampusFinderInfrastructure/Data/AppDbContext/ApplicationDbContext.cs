using _CampusFinderCore.Entities.UniversityEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderInfrastructure.Data.AppDbContext
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}

		public DbSet<University> Universities { get; set; }
		public DbSet<College> Colleges { get; set; }
		public DbSet<Major> Majors { get; set; }
		public DbSet<Diploma> Diplomas { get; set; }
		public DbSet<English_Test> EnglishTests { get; set; }
		public DbSet<College_English> CollegeEnglishTests { get; set; }
		public DbSet<College_Diploma> CollegeDiplomas { get; set; }
		public DbSet<Events> Events { get; set; }
	}
}
