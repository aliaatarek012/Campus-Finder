namespace CampusFinder.Dto.University_Dtos
{
    public class CollegeWithMajorsResponseDto
    {
        public int CollegeId { get; set; }
        public string CollegeName { get; set; }
        public decimal StandardFees { get; set; }
        public int UniversityId { get; set; }
        public string UniversityName { get; set; }
        public List<MajorDto> Majors { get; set; } = new List<MajorDto>();
    }
}
