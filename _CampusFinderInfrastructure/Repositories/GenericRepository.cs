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
			//if(typeof(T) == typeof(University))
			//{
			//	return await _dbcontext.Set<University>().Include(u => u.Colleges).ToListAsync() as IReadOnlyList<T>;
			//}
			return await _dbcontext.Set<T>().ToListAsync();
		}
		public async Task<T> GetByIdAsync(int id) 
		{
			return await _dbcontext.Set<T>().FindAsync(id);
		}
		public Task<T> AddAsync(T entity)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(T entity)
		{
			throw new NotImplementedException();
		}


		public Task UpdateAsync(T entity)
		{
			throw new NotImplementedException();
		}
	}
}
