using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderCore.Entities.UniversityEntities
{
	public class University : BaseEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int UniversityID { get; set; }



		public string? Description { get; set; }

		[StringLength(200)]
		public string? Location { get; set; }

		public string? RequiredDocuments { get; set; }

		public string? UniversityType { get; set; }

		public string? DegreeType { get; set; }

		public string? Rank { get; set; }

		public string? LearningStyle { get; set; }

		[EmailAddress]
		public string? UniEmail { get; set; }

		public int? UniPhone { get; set; }

		public string? PrimaryLanguage { get; set; }

		public string? WebsiteURL { get; set; }
		public string? PictureURL { get; set; }

		public ICollection<College> Colleges { get; set; }

	}
}
