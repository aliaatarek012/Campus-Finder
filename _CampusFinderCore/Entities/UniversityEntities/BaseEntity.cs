
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderCore.Entities
{
	public class BaseEntity
	{
		// A helper class for the entities to inherit from to avoid code duplication 
		// and to have a common base class for all entities
		[Required]
		[StringLength(100)]
		public string Name { get; set; } // Name is a common property for all entities in the database schema	
	}
}