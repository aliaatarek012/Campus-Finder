using _CampusFinderCore.Entities.UniversityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderCore.Specifications
{
    public class TopUniversitiesSpecification : BaseSpecifications<University>
    {
        public TopUniversitiesSpecification(int id) : base(u => u.UniversityID >= id && u.UniversityID <= 39) // id is assigned to use the parameterless ctor should be start at 30
        {
            Includes.Add(u => u.Colleges);
        }
    }
}
