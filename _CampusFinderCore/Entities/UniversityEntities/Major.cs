using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _CampusFinderCore.Entities
{
	public class Major : BaseEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int MajorID { get; set; }
		[ForeignKey("College")]
		public int CollegeID { get; set; }



		public string? Description { get; set; }

		public College College { get; set; }

	}
}