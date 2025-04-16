using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderCore.Entities.UniversityEntities
{
    public class Events : BaseEntity
    {
        [Key]
        public int Id { get; set; } // Primary Key

        [Display(Name = "Title")]
        public string Name { get; set; }

        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public string? Location { get; set; }
        public string? RegistrationLink { get; set; }
        public string? ContactEmail { get; set; }
        public string? PictureURL { get; set; }

        public bool IsFinished => Date.HasValue && Date.Value >= DateTime.UtcNow;
        // Nullable university relationship (for conferences or independent events)
        public int? UniversityID { get; set; }
        public University? University { get; set; }
    }
}
