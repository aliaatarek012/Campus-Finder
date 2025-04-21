using _CampusFinder.Controllers;
using _CampusFinder.Errors;
using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderCore.Repositories.Contract;
using _CampusFinderCore.Services.Contract;
using _CampusFinderInfrastructure.Data.AppDbContext;
using _CampusFinderInfrastructure.Specifications.University_Specs;
using AutoMapper;
using CampusFinder.Dto.University_Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampusFinder.Controllers
{
	
	public class UniversityController : BaseApiController
	{
		private readonly IGenericRepository<University> _universityRepo;
		private readonly IMapper _mapper;
        private readonly IUniversityService _universityService;
		private readonly ApplicationDbContext _context;

		public UniversityController(IGenericRepository<University> universityRepo, IMapper mapper, IUniversityService universityService, ApplicationDbContext context)
		{
			_universityRepo = universityRepo;
			_mapper = mapper;
            _universityService = universityService;
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<UniversityDto>>> GetUniversities([FromQuery] UniversitySpecParams specParams)
		{
			var universities = await _universityService.GetUniversitiesAsync(specParams);
			var universityDtos = _mapper.Map< IEnumerable<University>,IEnumerable<UniversityDto>>(universities);
			return Ok(universityDtos);
		}
		[HttpGet("HomePage")]
		public async Task<ActionResult<IEnumerable<HomePageDto>>> HomePage()
		{
			var universities = await _universityRepo.GetAllAsync();
			return Ok(_mapper.Map<IEnumerable<University> , IEnumerable<HomePageDto>>(universities));
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<University>> GetUniversity(int id)
		{
			var spec = new UniversityWithCollegesSpecifications(id);
			var university = await _universityRepo.GetEntityWithSpecAsync(spec);
			if (university == null)
			{
				return NotFound(new ApiResponse(404).ToDictionary());
			}
			var universityDtos = _mapper.Map<University, UniversityDto>(university);
			return Ok(universityDtos);
		}

		[HttpGet("college/{id}")]
		public async Task<ActionResult<CollegeDto>> GetCollege(int id)
		{


			var college = await _context.Colleges
				.Include(c => c.Majors)
				.Include(c => c.CollegeEnglishTests)
					.ThenInclude(cet => cet.English_Test)
				.Include(c => c.CollegeDiplomas)
					//.ThenInclude(cd => cd.Diploma)
				.FirstOrDefaultAsync(c => c.CollegeID == id);
			if (college == null)
			{
				return NotFound(new ApiResponse(404).ToDictionary());
			}

			var collegeDto = _mapper.Map<College, CollegeDto>(college);
			return Ok(collegeDto);
		}

	}
}
