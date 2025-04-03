using _CampusFinderCore.Entities.UniversityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderCore.Specifications
{
    public interface ISpecifications<T> where T : BaseEntity
    {
        //Criteria=> that carry value that will be sent to (Where)
        public Expression<Func<T, bool>> Criteria { get; set; }  //P=> P.Id==1

        //there More than (include) => so, i will use list 

        public List<Expression<Func<T, object>>> Includes { get; set; }

        //Two new Properties of Sorting 
        public Expression<Func<T, object>> OrderBy { get; set; }

        public Expression<Func<T, object>> OrderByDesc { get; set; }


    }
}
