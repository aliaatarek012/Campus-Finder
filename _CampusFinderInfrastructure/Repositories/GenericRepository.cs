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
			//if(typeof(T) == typeof(University))
			//{
			//	return await _dbcontext.Set<University>().Include(u => u.Colleges).ToListAsync() as IReadOnlyList<T>;
			//}
			return await _dbcontext.Set<T>().ToListAsync();
		}

        public async Task<T> GetByIdAsync(int id)
        {
            //if (typeof(T) == typeof(University))
            //{
            //	return await _dbcontext.Set<University>().Where(u => u.UniversityID == id).Include(u => u.Colleges).FirstOrDefaultAsync() as T;
            //}
            return await _dbcontext.Set<T>().FindAsync(id);
        }

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
            return SpecificationsEvaluator<T >.GetQuery(_dbcontext.Set<T>(), spec);
        }


        public async Task AddAsync(T entity)
        => await _dbcontext.AddAsync(entity);


        public void UpdateAsync(T entity)
        => _dbcontext.Update(entity);


        public void DeleteAsync(T entity)
          => _dbcontext.Remove(entity);

       
    }
}
