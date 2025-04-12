using _CampusFinderCore.Entities.UniversityEntities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CampusFinder.Dto.University_Dtos
{
	public class UniversityDto
	{
		public int UniversityID { get; set; }
		public string? Description { get; set; }

		public string? Name { get; set; }
		
		public string? Location { get; set; }

		public string? RequiredDocuments { get; set; }
		
		public string? UniversityType { get; set; }
	
		public string? DegreeType { get; set; }

		public string? Rank { get; set; }

		public string? LearningStyle { get; set; }

		
		public string? UniEmail { get; set; }
	
		public string? UniPhone { get; set; }
		
		public string? PrimaryLanguage { get; set; }
		
		public string? WebsiteURL { get; set; }
		
		public string? PictureURL { get; set; }

		public ICollection<College> Colleges { get; set; }

	}
}
