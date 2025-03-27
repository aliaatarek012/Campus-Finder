using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderCore.Repositories.Contract;
using _CampusFinderInfrastructure.Data.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderInfrastructure.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly ApplicationDbContext _dbcontext;

		public GenericRepository(ApplicationDbContext dbcontext)
        {
			_dbcontext = dbcontext;
		}
        public async  Task<IReadOnlyList<T>> GetAllAsync()
		{
			return await _dbcontext.Set<T>().ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id) 
		{
			return await _dbcontext.Set<T>().FindAsync(id);
		}
	}
}
