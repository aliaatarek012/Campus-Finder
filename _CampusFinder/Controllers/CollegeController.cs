using _CampusFinder.Controllers;
using _CampusFinder.Errors;
using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderCore.Repositories.Contract;
using _CampusFinderCore.Services.Contract;
using _CampusFinderInfrastructure.Specifications.University_Specs;
using AutoMapper;
using CampusFinder.Dto.University_Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace CampusFinder.Controllers
{
    public class CollegeController : BaseApiController
    {

        private readonly IMapper _mapper;
        private readonly ICollegeService _collegeService;
        private readonly ILogger<CollegeController> _logger;

        public CollegeController(
            IMapper mapper,
            ICollegeService collegeService,
            ILogger<CollegeController> logger)
        {
            _mapper = mapper;
            _collegeService = collegeService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollegeDto>>> GetColleges ([FromQuery] CollegeSpecParams collegeSpec)
        {
            var colleges = await _collegeService.GetCollegesWithSpecAsync(collegeSpec);
            var collegeDtos = _mapper.Map<IEnumerable<College>,IEnumerable<CollegeDto>>(colleges);
            return Ok(collegeDtos);
        }

        [HttpGet("by-university/{universityId}")]
        public async Task<ActionResult<IEnumerable<CollegeDto>>> GetCollegesByUniversityId(int universityId)
        {
            var colleges = await _collegeService.GetCollegesByUniversityIdAsync(universityId);
            var collegeDtos = _mapper.Map<IEnumerable<College>, IEnumerable<CollegeDto>>(colleges);
            return Ok(collegeDtos);
        }

        [HttpGet("{collegeId}")]
        public async Task<ActionResult<CollegeDto>> GetCollegeByID(int collegeId)
        {
          
            var college = await _collegeService.GetCollegeByIdAsync(collegeId);

            if (college == null)
            {
                return NotFound(new ApiResponse(404).ToDictionary());
            }
            var collegeDto = _mapper.Map<College, CollegeDto>(college);

            return Ok(collegeDto);

        }

        // Create college
        [HttpPost]
        public async Task<ActionResult<CollegeDto>> CreateCollege([FromBody]CollegeDto collegeDto)
        {
            var college = _mapper.Map<CollegeDto, College>(collegeDto);
            var createdCollege = await _collegeService.CreateCollegeAsync(college);
            var createdCollegeDto = _mapper.Map<College, CollegeDto>(createdCollege);
            return CreatedAtAction(nameof(GetCollegeByID), new { collegeId = createdCollege.CollegeID }, createdCollegeDto);
        }


        // Update college
        [HttpPut("{collegeId}")]
        public async Task<ActionResult<CollegeDto>> UpdateCollege(int collegeId, [FromBody] CollegeDto collegeDto)
        {
            if (collegeId != collegeDto.CollegeID)
            {
                return BadRequest(new ApiResponse(400).ToDictionary());
            }

            var existingCollege = await _collegeService.GetCollegeByIdAsync(collegeId);
            if (existingCollege == null)
            {
                return NotFound(new ApiResponse(404).ToDictionary());
            }

            var updatedCollege = _mapper.Map(collegeDto, existingCollege);
            await _collegeService.CreateCollegeAsync(updatedCollege);

            return NoContent();
        }

        // Delete college
        [HttpDelete("{collegeId}")]
        public async Task<ActionResult> DeleteCollege(int collegeId)
        {
            var college = await _collegeService.GetCollegeByIdAsync(collegeId);
            if (college == null)
            {
                return NotFound(new ApiResponse(404).ToDictionary());
            }
            await _collegeService.DeleteCollegeAsync(collegeId);
            return NoContent();
        }


    }
}