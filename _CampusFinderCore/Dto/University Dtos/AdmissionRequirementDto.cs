using CampusFinder.Dto.University_Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderCore.Dto.University_Dtos
{
    public class AdmissionRequirementDto
    {
        public int CollegeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal StandardFees { get; set; }
        public string YearsOfDration { get; set; }

        public List<DiplomaRequirementDto> Diplomas { get; set; }
        public List<EnglishTestRequirementDto> EnglishTests { get; set; }

        public List<MajorsDto> Majors { get; set; }
    }
}
