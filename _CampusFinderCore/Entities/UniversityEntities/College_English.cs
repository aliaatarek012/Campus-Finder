using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace _CampusFinderCore.Entities.UniversityEntities
{
	public class College_English
	{

		public int CollegeID { get; set; }
		public int TestID { get; set; }
		[Column(TypeName = "decimal(18,2)")]

		public decimal Min_Score { get; set; }

		public College College { get; set; }
		public English_Test English_Test { get; set; }

	}
}
