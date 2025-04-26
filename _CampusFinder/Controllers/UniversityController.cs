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
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace CampusFinder.Controllers
{

	public class UniversityController : BaseApiController
	{
		private readonly IGenericRepository<University> _universityRepo;
		private readonly IMapper _mapper;
		private readonly IUniversityService _universityService;
		private readonly ApplicationDbContext _context;
		private readonly ILogger<UniversityController> _logger;

		public UniversityController(IGenericRepository<University> universityRepo,
			IMapper mapper,
			IUniversityService universityService,
			ApplicationDbContext context,
			ILogger<UniversityController> logger)
		{
			_universityRepo = universityRepo;
			_mapper = mapper;
			_universityService = universityService;
			_context = context;
			_logger = logger;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<UniversityDto>>> GetUniversities([FromQuery] UniversitySpecParams specParams)
		{
			var universities = await _universityService.GetUniversitiesAsync(specParams);
			var universityDtos = _mapper.Map<IEnumerable<University>, IEnumerable<UniversityDto>>(universities);
			return Ok(universityDtos);
		}
		[HttpGet("HomePage")]
		public async Task<ActionResult<IEnumerable<HomePageDto>>> HomePage()
		{
			var universities = await _universityRepo.GetAllAsync();
			return Ok(_mapper.Map<IEnumerable<University>, IEnumerable<HomePageDto>>(universities));
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<UniversityDto>> GetUniversity(int id)
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


		[HttpPost]
		public async Task<ActionResult<UniversityDto>> CreateUniversity([FromBody] CreateUniversityDto createUniversityDto)
		{
			try {
				if (!ModelState.IsValid)
				{
					return BadRequest(new ApiResponse(400, "Invalid input data").ToDictionary());
				}

				var university = _mapper.Map<University>(createUniversityDto);
				await _universityService.CreateUniversityAsync(university);

				var universityDto = _mapper.Map<University, UniversityDto>(university);
				return CreatedAtAction(nameof(GetUniversity), new { id = university.UniversityID }, universityDto);

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error creating university");
				return StatusCode(500, new ApiResponse(500, ex.Message).ToDictionary());
			}
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<UniversityDto>> UpdateUniversity(int id, [FromBody] CreateUniversityDto updateUniversityDto)
		{
			
				if (!ModelState.IsValid)
				{
					return BadRequest(new ApiResponse(400, "Invalid input data.").ToDictionary());
				}

				var university = await _context.Universities
					.FirstOrDefaultAsync(u => u.UniversityID == id);

				if (university == null)
				{
					return NotFound(new ApiResponse(404).ToDictionary());
				}

				_mapper.Map(updateUniversityDto, university);
				await _context.SaveChangesAsync();

				var universityDto = _mapper.Map<UniversityDto>(university);
				return Ok(universityDto);

		}
		

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteUniversity(int id)
		{
			
			var university = await _universityService.GetUniversityAsync(id);
			if (university == null)
			{
				return NotFound(new ApiResponse(404).ToDictionary());
			}
			await _universityService.DeleteUniversityAsync(id);
			
			return NoContent();
	      	
			 
        }
	




		[HttpGet("college/{id}")]
		public async Task<ActionResult<CollegeDto>> GetCollege(int id)
		{


			
			var college = _context.Colleges
	            .Where(c => c.CollegeID == id)
	            .Select(c => new
	            {
	            	c.CollegeID,
	            	c.Name,
					c.Description,
	            	c.StandardFees,
					c.YearsOfDration,
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
