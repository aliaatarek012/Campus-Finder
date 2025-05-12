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
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UniversityController(
			IMapper mapper,
			IUniversityService universityService , 
		    IAdmissionRequirementService admissionRequirementService,
            IWebHostEnvironment webHostEnvironment)
			
			
		{
			_mapper = mapper;
			_universityService = universityService;
            _admissionRequirementService = admissionRequirementService;
            _webHostEnvironment = webHostEnvironment;
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



		[HttpGet("top10")]
		public async Task<ActionResult<IEnumerable<HomePageDto>>> GetTop10Universities()
		{
			var universities = await _universityService.GetTop10UniversitiesAsync();
			var universityDtos = _mapper.Map<IEnumerable<University>, IEnumerable<HomePageDto>>(universities);
			return Ok(universityDtos);
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
        public async Task<ActionResult<UniversityDto>> CreateUniversity([FromForm] CreateUniversityDto createUniversityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse(400, "Invalid input data").ToDictionary());
            }

            // Validate the picture file
            if (createUniversityDto.Picture != null)
            {
                var validImageTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                if (!validImageTypes.Contains(createUniversityDto.Picture.ContentType))
                {
                    return BadRequest(new ApiResponse(400, "Only JPEG, PNG, or  images are allowed").ToDictionary());
                }
                if (createUniversityDto.Picture.Length > 5 * 1024 * 1024) // 5MB limit
                {
                    return BadRequest(new ApiResponse(400, "Image size must not exceed 5MB").ToDictionary());
                }
            }

            // Save the image to wwwroot/images/university
            string pictureUrl = null;
            if (createUniversityDto.Picture != null)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "university");


                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(createUniversityDto.Picture.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await createUniversityDto.Picture.CopyToAsync(fileStream);
                }

                pictureUrl = $"/images/university/{uniqueFileName}";
            }

            // Map DTO to University entity
            var university = _mapper.Map<University>(createUniversityDto);
            university.PictureURL = pictureUrl; // Set the picture URL

            // Create university via service
            await _universityService.CreateUniversityAsync(university);

            // Map to DTO for response
            var universityDto = _mapper.Map<UniversityDto>(university);
            return CreatedAtAction(nameof(GetUniversity), new { id = university.UniversityID }, universityDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UniversityDto>> UpdateUniversity(int id, [FromForm] CreateUniversityDto updateUniversityDto)
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

            // Validate and handle picture file
            string pictureUrl = university.PictureURL; // Keep existing URL if no new image
            if (updateUniversityDto.Picture != null)
            {
                var validImageTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                if (!validImageTypes.Contains(updateUniversityDto.Picture.ContentType))
                {
                    return BadRequest(new ApiResponse(400, "Only JPEG, PNG, or images are allowed").ToDictionary());
                }
                if (updateUniversityDto.Picture.Length > 5 * 1024 * 1024) // 5MB limit
                {
                    return BadRequest(new ApiResponse(400, "Image size must not exceed 5MB").ToDictionary());
                }

                // Save new image (old image is not deleted)
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "university");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(updateUniversityDto.Picture.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await updateUniversityDto.Picture.CopyToAsync(fileStream);
                }

                pictureUrl = $"/images/university/{uniqueFileName}";
            }

            // Map DTO to entity and set PictureURL
            _mapper.Map(updateUniversityDto, university);
            university.PictureURL = pictureUrl;

            // Update university via service
            await _universityService.UpdateUniversityAsync(university);

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
