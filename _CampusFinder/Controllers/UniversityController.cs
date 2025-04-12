using _CampusFinder.Controllers;
using _CampusFinder.Errors;
using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderCore.Repositories.Contract;
using _CampusFinderCore.Services.Contract;
using _CampusFinderInfrastructure.Specifications.University_Specs;
using AutoMapper;
using CampusFinder.Dto.University_Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CampusFinder.Controllers
{
	
	public class UniversityController : BaseApiController
	{
		private readonly IGenericRepository<University> _universityRepo;
		private readonly IMapper _mapper;
        private readonly IUniversityService _universityService;

        public UniversityController(IGenericRepository<University> universityRepo,IMapper mapper , IUniversityService universityService)
		{
			_universityRepo = universityRepo;
			_mapper = mapper;
            _universityService = universityService;
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
			var university = await _universityRepo.GetByIdAsync(id);
			if (university == null)
			{
				return NotFound(new ApiResponse(404).ToDictionary());
			}
			var universityDtos = _mapper.Map<University, UniversityDto>(university);
			return Ok(universityDtos);
		}
	}
}
