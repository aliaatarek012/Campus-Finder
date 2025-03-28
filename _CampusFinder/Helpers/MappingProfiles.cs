using _CampusFinderCore.Entities.UniversityEntities;
using AutoMapper;
using CampusFinder.Dto.University_Dtos;

namespace CampusFinder.Helpers
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<University, HomePageDto>()
                .ForMember(u => u.UniversityId, o => o.MapFrom(s => s.UniversityID))
                .ForMember(u => u.Name, o => o.MapFrom(s => s.Name))
                .ForMember(u => u.Location, o => o.MapFrom(s => s.Location))
                .ForMember(u => u.PictureURL, o => o.MapFrom(s => s.PictureURL));
                
        }
    }
}
