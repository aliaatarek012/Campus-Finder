using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _CampusFinderCore.Entities.UniversityEntities
{
	public class College : BaseEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CollegeID { get; set; }



		[ForeignKey("University")]
		public int UniversityID { get; set; }

		public string? Description { get; set; }
		[Column(TypeName = "decimal(18,2)")]

		public decimal StandardFees { get; set; }

		public University University { get; set; }

		public ICollection<Major>? Majors { get; set; }
		public ICollection<College_Diploma>? CollegeDiplomas { get; set; }

		public ICollection<College_English>? CollegeEnglishTests { get; set; }
	}
}