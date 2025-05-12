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
            CreateMap<University, CreateUniversityDto>();
            CreateMap<CreateUniversityDto, University>()
            .ForMember(dest => dest.PictureURL, opt => opt.Ignore()); // Handled in controller
            CreateMap<College, CollegeBasicDto>()
                .ForMember(u => u.PictureURL, o => o.MapFrom<CollegePictureResolver<CollegeBasicDto>>());
            CreateMap<College, CollegeDto>()
                .ForMember(d => d.YearsOfDuration ,o => o.MapFrom(s => s.YearsOfDration))
                .ForMember(u => u.PictureURL, o => o.MapFrom<CollegePictureResolver<CollegeDto>>());

            CreateMap<CollegeDto, College>()
                .ForMember(d => d.YearsOfDration, o => o.MapFrom(s => s.YearsOfDuration));
            CreateMap<Major, MajorDto>()
                .ForMember(d => d.MajorId, o => o.MapFrom(s => s.MajorID))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
                .ForMember(d => d.CollegeName , o => o.MapFrom(s => s.College.Name))
                .ForMember(d => d.CollegeId, o => o.MapFrom(s => s.College.CollegeID));
            CreateMap<MajorDto, Major>()
                .ForMember(d => d.MajorID, o => o.MapFrom(s => s.MajorId))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
                .ForMember(d => d.CollegeID, o => o.MapFrom(s => s.CollegeId));
			//CreateMap<College_English, EnglishTestRequirementDto>()
			//	.ForMember(dest => dest.TestName, opt => opt.MapFrom(src => src.English_Test.Name))
			//	.ForMember(dest => dest.MinScore, opt => opt.MapFrom(src => src.Min_Score));

			//CreateMap<College_Diploma, DiplomaRequirementDto>()
			//	.ForMember(dest => dest.DiplomaName, opt => opt.MapFrom(src => src.Diploma.Name))
			//	.ForMember(dest => dest.MinGrade, opt => opt.MapFrom(src => src.Min_Grade))
			//	.ForMember(dest => dest.Requirements, opt => opt.MapFrom(src => src.Requirments));

            CreateMap<Diploma, DiplomaDto>();
            CreateMap<DiplomaDto, Diploma>();

            CreateMap<Events, EventsDto>()
                //.ForMember(d => d.UniversityName, o => o.MapFrom(s => s.University.Name))
                .ForMember(d => d.PictureURL , o => o.MapFrom<EventsPictureResolver<EventsDto>>())
                ;
            CreateMap<Events, EventsHomeDto>()
				.ForMember(d => d.PictureURL, o => o.MapFrom<EventsPictureResolver<EventsHomeDto>>());
            //Unused
			CreateMap<University, UniversityWithCollegesDto>()
                .ForMember(d => d.UniversityID, o => o.MapFrom(s => s.UniversityID))
                .ForMember(d => d.UniversityName, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Colleges, o => o.Ignore()); // we'll set it manually when grouping

			CreateMap<College, CollegeWithMajorsResponseDto>()
                .ForMember(d => d.CollegeId, o => o.MapFrom(s => s.CollegeID))
                .ForMember(d => d.CollegeName, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.StandardFees, o => o.MapFrom(s => s.StandardFees))
                .ForMember(d => d.Majors, o => o.MapFrom(s => s.Majors));

        }
    }
}
