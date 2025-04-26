using _CampusFinder.Controllers;
using _CampusFinder.Errors;
using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderCore.Services.Contract;
using _CampusFinderCore.Specifications.University_Specs;
using AutoMapper;
using CampusFinder.Dto.University_Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CampusFinder.Controllers
{

    public class MajorController : BaseApiController
    {
        private readonly IMajorService _majorService;
        private readonly IMapper _mapper;

        public MajorController(IMajorService majorService, IMapper autoMapper)
        {
            _majorService = majorService;
            _mapper = autoMapper;
        }

        // Get all majors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MajorDto>>> GetMajors([FromQuery] MajorsSpecParams specParams)
        {
            var majors = await _majorService.GetMajorsAsync(specParams);
            var majorDtos = _mapper.Map<IEnumerable<Major>, IEnumerable<MajorDto>>(majors);
            return Ok(majorDtos);
        }

        // Get a major by ID
        [HttpGet("{majorId}")]
        public async Task<ActionResult<MajorDto>> GetMajor(int majorId)
        {
            var major = await _majorService.GetMajorAsync(majorId);
            if (major == null)
            {
                return NotFound(new ApiResponse(404).ToDictionary());
            }
            var majorDto = _mapper.Map<Major, MajorDto>(major);
            return Ok(majorDto);
        }

        // Get majors by college ID
        [HttpGet("by-college/{collegeId}")]
        public async Task<ActionResult<IEnumerable<MajorDto>>> GetMajorsByCollegeId(int collegeId)
        {
            var majors = await _majorService.GetMajorsByCollegeIdAsync(collegeId);
            var majorDtos = _mapper.Map<IReadOnlyList<Major>, IReadOnlyList<MajorDto>>(majors);
            return Ok(majorDtos);
        }

        // Create a new major
        [HttpPost]
        public async Task<ActionResult<MajorDto>> CreateMajor([FromBody]MajorDto majorDto)
        {
            var major = _mapper.Map<MajorDto, Major>(majorDto);
            var createdMajor = await _majorService.CreateMajorAsync(major);
            var createdMajorDto = _mapper.Map<Major, MajorDto>(createdMajor);
            return CreatedAtAction(nameof(GetMajor), new { majorId = createdMajor.MajorID }, createdMajorDto);
        }

        // Delete a major by ID
        [HttpDelete("{majorId}")]
        public async Task<IActionResult> DeleteMajor(int majorId)
        {
            var major = await _majorService.GetMajorAsync(majorId);
            if (major == null)
            {
                return NotFound(new ApiResponse(404).ToDictionary());
            }
            await _majorService.DeleteMajorAsync(majorId);
            return NoContent();
        }

        // Update a major
        [HttpPut("{majorId}")]
        public async Task<IActionResult> UpdateMajor(int majorId,[FromBody] MajorDto majorDto)
        {
            if (majorId != majorDto.MajorId)
            {
                return BadRequest(new ApiResponse(400).ToDictionary());
            }

            var existingMajor = await _majorService.GetMajorAsync(majorId);
            if (existingMajor == null)
            {
                return NotFound(new ApiResponse(404).ToDictionary());
            }

            var updatedMajor = _mapper.Map(majorDto, existingMajor);
            await _majorService.CreateMajorAsync(updatedMajor);

            return NoContent();
        }
    }
}
