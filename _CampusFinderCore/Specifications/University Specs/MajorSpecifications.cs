using _CampusFinderCore.Entities.UniversityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderCore.Specifications.University_Specs
{
    public class MajorSpecifications : BaseSpecifications<Major>
    {
        public MajorSpecifications(MajorsSpecParams majorsSpec) : base(
            
            x => (string.IsNullOrEmpty(majorsSpec.Search) || x.Name.ToLower().Contains(majorsSpec.Search)) &&
            (!majorsSpec.CollegeId.HasValue || x.CollegeID == majorsSpec.CollegeId) &&
            (!majorsSpec.UniversityId.HasValue || x.College.UniversityID == majorsSpec.UniversityId)
        )
        {
            Includes.Add(x => x.College);
            AddOrderBy(x => x.Name);
           

            if (!string.IsNullOrEmpty(majorsSpec.Sort))
            {
                switch (majorsSpec.Sort)
                {
                    case "name":
                        AddOrderBy(p => p.Name);
                        break;
                    default:
                        AddOrderBy(p => p.MajorID);
                        break;
                }
            }
        }

        public MajorSpecifications(int id) : base(x => x.MajorID == id)
        {
            Includes.Add(x => x.College);
        }



    }
}
