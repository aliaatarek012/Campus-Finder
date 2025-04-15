using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderCore.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderCore.Specifications
{
    public interface IUnitOfWork : IAsyncDisposable
    {

        ///Signature for Generic Method , Any body interact with this Method
        ///=>Will return IGenericRepository<Entity>, Entity that is needed

        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;


        //Method simulate Method that Savechanges that exist at Dbcontext,return Number of Rows that made Row affected  
        Task<int> CompleteAsync();
    
    }
}
