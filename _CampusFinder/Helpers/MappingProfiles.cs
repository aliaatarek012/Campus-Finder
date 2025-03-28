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
                .ForMember(u => u.PictureURL, o => o.MapFrom<UniPictureUrlResolver<HomePageDto>>());

			CreateMap<University, UniversityDto>()
				.ForMember(u => u.UniversityID, o => o.MapFrom(s => s.UniversityID))
				.ForMember(u => u.Description, o => o.MapFrom(s => s.Description))
				.ForMember(u => u.Name, o => o.MapFrom(s => s.Name))
				.ForMember(u => u.RequiredDocuments, o => o.MapFrom(s => s.RequiredDocuments))
				.ForMember(u => u.UniversityType, o => o.MapFrom(s => s.UniversityType))
				.ForMember(u => u.DegreeType, o => o.MapFrom(s => s.DegreeType))
				.ForMember(u => u.LearningStyle, o => o.MapFrom(s => s.LearningStyle))
				.ForMember(u => u.UniEmail, o => o.MapFrom(s => s.UniEmail))
				.ForMember(u => u.UniPhone, o => o.MapFrom(s => s.UniPhone))
				.ForMember(u => u.PrimaryLanguage, o => o.MapFrom(s => s.PrimaryLanguage))
				.ForMember(u => u.WebsiteURL, o => o.MapFrom(s => s.WebsiteURL))
				.ForMember(u => u.PictureURL, o => o.MapFrom<UniPictureUrlResolver<UniversityDto>>())
				.ForMember(u => u.Colleges, o => o.MapFrom(s => s.Colleges));
		}
    }
}
