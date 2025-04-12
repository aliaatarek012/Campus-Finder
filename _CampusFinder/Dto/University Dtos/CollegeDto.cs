using _CampusFinderCore.Entities.UniversityEntities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CampusFinder.Dto.University_Dtos
{
	public class CollegeDto
	{
        public int CollegeID { get; set; }

        public int UniversityID { get; set; }

        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [JsonPropertyName("Standard Fees")]
        public decimal StandardFees { get; set; }

        public University University { get; set; }

        public ICollection<Major>? Majors { get; set; }
        public ICollection<College_Diploma>? CollegeDiplomas { get; set; }

        public ICollection<College_English>? CollegeEnglishTests { get; set; }
    }
}