using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderCore.Repositories.Contract;
using _CampusFinderCore.Specifications;
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

        //Two Methods Operate with Specification Design Pattern(Dynamic way)
        //When we Use two methods ? when are there Navigational property that we need to make it (Include)
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecifications(spec).ToListAsync();

        }

        public async Task<T?> GetEntityWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }



        //To avoid repeating Code(to easy use)  
        private IQueryable<T> ApplySpecifications(ISpecifications<T> spec)
        {
            return SpecificationsEvaluator<T>.GetQuery(_dbcontext.Set<T>(), spec);
        }
    }
}
