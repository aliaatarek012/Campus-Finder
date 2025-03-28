using _CampusFinder.Controllers;
using _CampusFinder.Errors;
using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderCore.Repositories.Contract;
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

		public UniversityController(IGenericRepository<University> universityRepo,IMapper mapper)
		{
			_universityRepo = universityRepo;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<University>>> GetUniversities()
		{
			var universities = await _universityRepo.GetAllAsync();
			return Ok(universities);
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
			return Ok(university);
		}
	}
}
