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


			//var college = await _context.Colleges
			//	//.Include(c => c.Majors)
			//	.Include(c => c.CollegeEnglishTests)
			//		.ThenInclude(cet => cet.English_Test)
			//	.Include(c => c.CollegeDiplomas)
			//		//.ThenInclude(cd => cd.Diploma)
			//	.FirstOrDefaultAsync(c => c.CollegeID == id);
			//var result = from c in _context.Colleges
			//			 join ct in _context.CollegeEnglishTests on c.CollegeID equals ct.CollegeID
			//			 join et in _context.EnglishTests on ct.TestID equals et.TestID
			//			 where c.CollegeID == id
			//			 select new
			//			 {
			//				 c.CollegeID,
			//				 c.Name,
			//				 c.StandardFees,
			//				 ct.TestID,
			//				 EnglishTestName = et.Name,
			//				 ct.Min_Score
			//			 };
			var college = _context.Colleges
	            .Where(c => c.CollegeID == id)
	            .Select(c => new
	            {
	            	c.CollegeID,
	            	c.Name,
					c.Description,
	            	c.StandardFees,
				     Diplomas = _context.CollegeDiplomas.
					 Where(cd => cd.CollegeID == c.CollegeID)
					 .Select( cd => new
						{
							DiplomaName = cd.Diploma.Name,
							cd.Min_Grade,
							cd.Requirments,
						}).ToList(),

	            	EnglishTests = _context.CollegeEnglishTests
	            		.Where(ct => ct.CollegeID == c.CollegeID)
	            		.Join(_context.EnglishTests,
	            			  ct => ct.TestID,
	            			  et => et.TestID,
	            			  (ct, et) => new
	            			  {
	            				  ct.TestID,
	            				  EnglishTestName = et.Name,
	            				  ct.Min_Score
	            			  }).ToList() ,


					 Majors = _context.Majors
					.Where(m => m.CollegeID == c.CollegeID)
					.Select(m => new
					{
						m.Name,
						m.Description,
					}).ToList()
	            })
	            .FirstOrDefault();


			if (college == null)
			{
				return NotFound(new ApiResponse(404).ToDictionary());
			}

			//var collegeDto = _mapper.Map<College, CollegeDto>(college);
			return Ok(college);
		}

	}
}
