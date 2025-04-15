namespace CampusFinder.Dto.University_Dtos
{
    public class UniversityWithCollegesDto
    {
        public int UniversityID { get; set; }
        public string UniversityName { get; set; }
        public List<CollegeWithMajorsResponseDto> Colleges { get; set; }
    }
}
