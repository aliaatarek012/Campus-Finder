using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderCore.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderInfrastructure.Specifications.University_Specs
{
    public class CollegeWithMajorsSpecifications : BaseSpecifications<College>
    {
        public CollegeWithMajorsSpecifications(CollegeSpecParams specParams)
        : base(c =>
             (!specParams.MinFees.HasValue || c.StandardFees >= specParams.MinFees.Value) &&
        (!specParams.MaxFees.HasValue || c.StandardFees <= specParams.MaxFees.Value) &&
        (!specParams.UniversityId.HasValue || c.UniversityID == specParams.UniversityId.Value) &&
        (string.IsNullOrEmpty(specParams.CollegeName) || c.Majors.Any(m => m.Name.Contains(specParams.CollegeName)))
        )

        {
            // Include related majors
            Includes.Add(c => c.Majors);

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "feesAsc":
                        AddOrderBy(c => c.StandardFees);
                        break;

                    case "feesDesc":
                        AddOrderByDesc(c => c.StandardFees);
                        break;

                    default:
                        AddOrderBy(c => c.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(c => c.Name);
            }

        }

        // Constructor for fetching a specific college by ID
        public CollegeWithMajorsSpecifications(int id)
            : base(c => c.CollegeID == id)
        {
            Includes.Add(c => c.Majors);

        } 

    }
}
