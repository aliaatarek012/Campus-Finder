using _CampusFinder.Controllers;
using _CampusFinder.Errors;
using _CampusFinderCore.Dto.University_Dtos;
using _CampusFinderCore.Services.Contract;
using _CampusFinderCore.Specifications;
using CampusFinder.Dto.University_Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CampusFinder.Controllers
{
    public class AdmissionsRequirmentsController : BaseApiController
    {
        private readonly IAdmissionRequirementService _admissionRequirementService;

        public AdmissionsRequirmentsController(IAdmissionRequirementService admissionRequirementService)
        {
            _admissionRequirementService = admissionRequirementService;
        }

        [HttpGet("{collegeId}")]
        public async Task<ActionResult<AdmissionRequirementDto>> GetAllAdmissionsRequirementByCollegeId(int collegeId)
        {
            var admissionRequirements = await _admissionRequirementService.GetAllAdmissionsRequirementByCollegeIdAsync(collegeId);
            if (admissionRequirements == null)
            {
                return NotFound(new ApiResponse(404).ToDictionary());
            }
            return Ok(admissionRequirements);
        }

        [HttpGet("DiplomaRequirments/{collegeId}")]
        public async Task<ActionResult<List<DiplomaRequirementDto>>> GetDiplomaRequirements(int collegeId)
        {
            var diplomaRequirements = await _admissionRequirementService.GetDiplomaRequirementsAsync(collegeId);
            if (diplomaRequirements == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(diplomaRequirements);
        }

        [HttpGet("EnglishRequirments/{collegeId}")]
        public async Task<ActionResult<List<EnglishTestRequirementDto>>> GetEnglishTestRequirements(int collegeId)
        {
            var englishTestRequirements = await _admissionRequirementService.GetEnglishTestRequirementsAsync(collegeId);
            if (englishTestRequirements == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(englishTestRequirements);
        }
    }
}
