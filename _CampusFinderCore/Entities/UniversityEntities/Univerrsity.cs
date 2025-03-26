using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace _CampusFinderCore.Entities.UniversityEntities
{
	public class University : BaseEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int UniversityID { get; set; }


		[JsonPropertyName("Description")]
		public string? Description { get; set; }

		[StringLength(200)]
		[JsonPropertyName("Location")]	
		public string? Location { get; set; }

		[JsonPropertyName("Required Documents")] // To Match the json file with its column on seeding data
		public string? RequiredDocuments { get; set; }
		[JsonPropertyName("UniversityType")]
		public string? UniversityType { get; set; }
		[JsonPropertyName("Degree Type")]
		public string? DegreeType { get; set; }
		[JsonPropertyName("Rank")]	
		public string? Rank { get; set; }
		[JsonPropertyName("LearningStyle")]
		public string? LearningStyle { get; set; }

		[EmailAddress]
		[JsonPropertyName("UniEmail")]
		public string? UniEmail { get; set; }
		[JsonPropertyName("UniPhone")]
		public string? UniPhone { get; set; }
		[JsonPropertyName("PrimaryLanguage")]
		public string? PrimaryLanguage { get; set; }
		[JsonPropertyName("Website URL")]
		public string? WebsiteURL { get; set; }
		[JsonPropertyName("Picture URL")]
		public string? PictureURL { get; set; }

		public ICollection<College> Colleges { get; set; }

	}
}
