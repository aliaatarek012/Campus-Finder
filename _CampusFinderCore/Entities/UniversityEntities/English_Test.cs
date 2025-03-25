using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderCore.Entities.UniversityEntities
{
	public class English_Test : BaseEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int TestID { get; set; }



		public ICollection<College_English> CollegeEnglishTests { get; set; }

	}
}