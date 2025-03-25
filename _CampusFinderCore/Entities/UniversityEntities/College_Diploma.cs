using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _CampusFinderCore.Entities
{
	public class College_Diploma
	{

		[ForeignKey("College")]
		public int CollegeID { get; set; }

		[ForeignKey("Diploma")]
		public int DiplomaID { get; set; }
		public string Min_Grade { get; set; } // string cauz it can be Character , decimal or percentage 

		public string? Requirments { get; set; }

		public College College { get; set; }
		public Diploma Diploma { get; set; }
	}
}