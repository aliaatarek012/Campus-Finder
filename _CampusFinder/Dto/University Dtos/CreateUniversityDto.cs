using System.ComponentModel.DataAnnotations;

namespace CampusFinder.Dto.University_Dtos
{
    public class CreateUniversityDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }
        public string RequiredDocuments { get; set; }
        [Required]
        public string UniversityType { get; set; }
        [Required]
        public string DegreeType { get; set; }
        public string Rank { get; set; }
        public string LearningStyle { get; set; }
        [EmailAddress]
        public string UniEmail { get; set; }
        public string? UniPhone { get; set; }
        public string PrimaryLanguage { get; set; }
        [Url]
        public string WebsiteURL { get; set; }
        
        public IFormFile Picture { get; set; }
    }
}
