namespace CampusFinder.Dto.University_Dtos
{
    public class MajorDto
    {
        public int MajorId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int CollegeId { get; set; }

        public string CollegeName { get; set; }
    }
}
