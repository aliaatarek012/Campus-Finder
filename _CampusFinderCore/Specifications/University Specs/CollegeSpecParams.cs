using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderInfrastructure.Specifications.University_Specs
{
    public class CollegeSpecParams 
    {

        private string? search;
        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }

        public string? Sort { get; set; }
        public string? CollegeName { get; set; }
        public decimal? MinFees { get; set; }
        public decimal? MaxFees { get; set; }

        // Add a property for filtering by university
        public int? UniversityId { get; set; }
    }
}
