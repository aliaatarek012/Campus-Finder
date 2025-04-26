using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderCore.Specifications.University_Specs
{
    public class MajorsSpecParams
    {
        public MajorsSpecParams() { }

        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }

        public string? Sort { get; set; }
        public string? MajorName { get; set; }
        public int? CollegeId { get; set; }
        public int? UniversityId { get; set; }
    }
}
