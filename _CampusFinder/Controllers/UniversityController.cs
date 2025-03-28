using _CampusFinder.Controllers;
using _CampusFinder.Errors;
using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderCore.Repositories.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CampusFinder.Controllers
{
	
	public class UniversityController : BaseApiController
	{
		private readonly IGenericRepository<University> _universityRepo;

		public UniversityController(IGenericRepository<University> universityRepo)
		{
			_universityRepo = universityRepo;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<University>>> GetUniversities()
		{
			var universities = await _universityRepo.GetAllAsync();
			return Ok(universities);
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
