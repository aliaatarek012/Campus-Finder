using _CampusFinder.Controllers;
using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderCore.Repositories.Contract;
using _CampusFinderInfrastructure.Specifications.University_Specs;
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
		public async Task<ActionResult<IEnumerable<University>>> GetUniversities([FromQuery] UniversitySpecParams specParams)
		{
            // Create a specification for filtering universities
            var spec = new UniversityWithCollegesSpecifications(specParams);
            var universities = await _universityRepo.GetAllAsync();
			return Ok(universities);
		}
	}
}
