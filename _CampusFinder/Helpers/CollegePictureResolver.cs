using _CampusFinderCore.Entities.UniversityEntities;
using AutoMapper;

namespace CampusFinder.Helpers
{
    public class CollegePictureResolver<TDestination> : IValueResolver<College, TDestination, string>
    {
        private readonly IConfiguration _configuration;

        public CollegePictureResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string Resolve(College source, TDestination destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureURL))
            {
                return $"{_configuration["ApiBaseUrl"]}/{source.PictureURL}";
            }
            return string.Empty;
        }
    }
}
