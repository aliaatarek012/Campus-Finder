using _CampusFinder.Controllers;
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
	}
}
