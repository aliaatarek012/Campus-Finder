namespace CampusFinder.Dto.University_Dtos
{
	public class EventsDto
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string? Description { get; set; }

		public DateTime? Date { get; set; }

		public string? Location { get; set; }

		public string? RegistrationLink { get; set; }

		public string? ContactEmail { get; set; }

		public string? PictureURL { get; set; }

		public int? UniversityID { get; set; }

		//public string? UniversityName { get; set; }

	}
}
