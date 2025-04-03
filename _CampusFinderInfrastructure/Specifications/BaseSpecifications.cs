using _CampusFinderCore.Entities.UniversityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderCore.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
    {
        //Used for filtering data.
        public Expression<Func<T, bool>> Criteria { get; set; } = null;

        ///The Includes property is a list of include expressions,
        ///indicating related entities that should be eagerly loaded with the main entity.
        ///This helps to avoid the N+1 query problem when fetching related data.

        //Initialize Includes => Empty List
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();                                                                                                                     //
        //n for sorting data in ascending order.
        public Expression<Func<T, object>> OrderBy { get; set; } = null;
        //for sorting data in descending order.
        public Expression<Func<T, object>> OrderByDesc { get; set; } = null;

        //Use cons to Create object from BaseSpecifications and Make Criteria is null and build Query Get all Universities 
        public BaseSpecifications()
        {
            // Criteria = null;
        }

        //Use cons to Create object from BaseSpecifications and Make Criteria is value and build Query Get Specific University

        public BaseSpecifications(Expression<Func<T, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;  
        }

    }
}
