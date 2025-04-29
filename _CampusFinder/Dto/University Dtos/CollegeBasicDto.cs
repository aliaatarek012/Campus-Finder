namespace CampusFinder.Dto.University_Dtos
{
	public class CollegeBasicDto // This DTO is used in the page of the university , the university view its colleges names
	{
		public int CollegeID { get; set; }
		public string Name { get; set; }

		public string YearsOfDration { get; set; }

		public string Description { get; set; }
        public string PictureURL { get; set; }

    }
}
