using _CampusFinderCore.Entities.UniversityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Twilio.TwiML.Voice;

namespace _CampusFinderCore.Specifications.University_Specs
{
    // This is used to return all the entities that are related to a given entity by a foreign key.
    // For Example , return all Colleges has same universityId Or all Majors has same CollegeID
    public class RelatedEntitiesByForeignKeySpec<TEntity> : BaseSpecifications<TEntity> where TEntity : BaseEntity
    {
        public RelatedEntitiesByForeignKeySpec(Expression<Func<TEntity, bool>> criteria,params Expression<Func<TEntity, object>>[] includes)
            : base(criteria)
        {
            foreach (var include in includes)
            {
                Includes.Add(include);
            }
        }

       

    }
}
