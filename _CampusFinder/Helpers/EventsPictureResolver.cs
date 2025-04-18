using _CampusFinderCore.Entities.UniversityEntities;
using AutoMapper;
using CampusFinder.Dto.University_Dtos;

namespace CampusFinder.Helpers
{
	public class EventsPictureResolver<TDestination> : IValueResolver<Events, TDestination, string>
	{
		private readonly IConfiguration _configuration;

		public EventsPictureResolver(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string Resolve(Events source, TDestination destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.PictureURL))
			{
				return $"{_configuration["ApiBaseUrl"]}/{source.PictureURL}";
			}
			return string.Empty;
		}
	}
}
