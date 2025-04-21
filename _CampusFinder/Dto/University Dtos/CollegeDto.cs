using _CampusFinderCore.Entities.UniversityEntities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CampusFinder.Dto.University_Dtos
{
	public class CollegeDto
	{
        public int CollegeID { get; set; }
		public string Name { get; set; }
		

        public string? Description { get; set; }

        
        public decimal StandardFees { get; set; }


        public List<MajorDto> Majors { get; set; }
        public List<EnglishTestRequirementDto> EnglishTestRequirements { get; set; }

        public List<DiplomaRequirementDto> DiplomaRequirements { get; set; }
    }
}