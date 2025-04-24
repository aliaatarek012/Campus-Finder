using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderCore.Repositories.Contract
{
	public interface IGenericRepository <T> where T : BaseEntity
	{
		Task<T> GetByIdAsync(int id);
		Task<IReadOnlyList<T>> GetAllAsync();

        Task AddAsync(T entity);


        void UpdateAsync(T entity);


        void DeleteAsync(T entity);


        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);

        Task<T?> GetEntityWithSpecAsync(ISpecifications<T> spec);
       

    }
}
