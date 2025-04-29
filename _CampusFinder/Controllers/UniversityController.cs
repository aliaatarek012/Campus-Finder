using _CampusFinder.Controllers;
using _CampusFinder.Errors;
using _CampusFinderCore.Dto.University_Dtos;
using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderCore.Repositories.Contract;
using _CampusFinderCore.Services.Contract;
using _CampusFinderInfrastructure.Data.AppDbContext;
using _CampusFinderInfrastructure.Specifications.University_Specs;
using _CampusFinderService;
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
		private readonly IMapper _mapper;
		private readonly IUniversityService _universityService;
        private readonly IAdmissionRequirementService _admissionRequirementService;

        public UniversityController(
			IMapper mapper,
			IUniversityService universityService , 
		    IAdmissionRequirementService admissionRequirementService)
			
			
		{
			_mapper = mapper;
			_universityService = universityService;
            _admissionRequirementService = admissionRequirementService;
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
			var universities = await _universityService.GetUniversitiesAsync();
			return Ok(_mapper.Map<IEnumerable<University>, IEnumerable<HomePageDto>>(universities));
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<UniversityDto>> GetUniversity(int id)
		{
			var spec = new UniversityWithCollegesSpecifications(id);
			var university = await _universityService.GetUniversityBySpecAsync(spec);
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
			
				if (!ModelState.IsValid)
				{
					return BadRequest(new ApiResponse(400, "Invalid input data").ToDictionary());
				}

				var university = _mapper.Map<University>(createUniversityDto);
				await _universityService.CreateUniversityAsync(university);

				var universityDto = _mapper.Map<University, UniversityDto>(university);
				return CreatedAtAction(nameof(GetUniversity), new { id = university.UniversityID }, universityDto);

			
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<UniversityDto>> UpdateUniversity(int id, [FromBody] CreateUniversityDto updateUniversityDto)
		{
			
				if (!ModelState.IsValid)
				{
					return BadRequest(new ApiResponse(400, "Invalid input data.").ToDictionary());
				}

           var university = await _universityService.GetUniversityAsync(id);
            if (university == null)
				{
					return NotFound(new ApiResponse(404).ToDictionary());
				}

				_mapper.Map(updateUniversityDto, university);
            await _universityService.CreateUniversityAsync(university); 


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
	




		[HttpGet("college/{collegeId}")]
        public async Task<ActionResult<AdmissionRequirementDto>> GetAllAdmissionsRequirementByCollegeId(int collegeId)
        {
            var admissionRequirements = await _admissionRequirementService.GetAllAdmissionsRequirementByCollegeIdAsync(collegeId);
            if (admissionRequirements == null)
            {
                return NotFound(new ApiResponse(404).ToDictionary());
            }
            return Ok(admissionRequirements);
        }

    }
}
