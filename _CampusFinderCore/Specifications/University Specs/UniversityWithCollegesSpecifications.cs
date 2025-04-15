using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderInfrastructure.Specifications.University_Specs
{
    public class UniversityWithCollegesSpecifications : BaseSpecifications<University>
    {
        // Constructor for fetching all universities with filters
        public UniversityWithCollegesSpecifications(UniversitySpecParams specParams)
            : base(u =>
                (string.IsNullOrEmpty(specParams.Search) || u.Name.Contains(specParams.Search)) &&
        (string.IsNullOrEmpty(specParams.Location) || u.Location.Contains(specParams.Location)) &&
        (string.IsNullOrEmpty(specParams.UniversityType) || u.UniversityType == specParams.UniversityType))
        {
            // Sorting logic
            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "rankAsc":
                        AddOrderBy(u => u.Rank);
                        break;

                    case "rankDesc":
                        AddOrderByDesc(u => u.Rank);
                        break;

                    default:
                        AddOrderBy(u => u.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(u => u.Name);
            }

     

        }

        
    }
}
