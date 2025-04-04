﻿using _CampusFinderCore.Entities.UniversityEntities;
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
                (string.IsNullOrEmpty(specParams.Search) || u.Name.ToLower().Contains(specParams.Search)) &&
                (string.IsNullOrEmpty(specParams.Location) || u.Location.Contains(specParams.Location, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(specParams.UniversityType) || u.UniversityType.Equals(specParams.UniversityType, StringComparison.OrdinalIgnoreCase))
            )
        {
            // Include related colleges
            Includes.Add(u => u.Colleges);
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

        // Constructor for fetching a specific university by ID
        public UniversityWithCollegesSpecifications(int id)
        : base(u => u.UniversityID == id)
        {
            Includes.Add(u => u.Colleges);
            Includes.Add(u => u.Colleges.Select(c => c.Majors));
        }
    }
}
