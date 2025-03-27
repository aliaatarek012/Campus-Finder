using _CampusFinderCore.Entities.UniversityEntities;
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
		//Task<T> AddAsync(T entity);
		//Task UpdateAsync(T entity);
		//Task DeleteAsync(T entity);
	}
}
