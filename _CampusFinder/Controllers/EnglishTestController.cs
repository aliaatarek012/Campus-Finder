using _CampusFinder.Controllers;
using _CampusFinder.Errors;
using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderCore.Services.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CampusFinder.Controllers
{
    public class EnglishTestController : BaseApiController
    {
        private readonly IEnglishTestService _englishTestService;

        public EnglishTestController(IEnglishTestService englishTestService)
        {
            _englishTestService = englishTestService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<English_Test>>> GetAllTests()
        {
            var englishTests = await _englishTestService.GetAllTests();
            if (englishTests == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(englishTests);
        }

        [HttpGet("{testId}")]
        public async Task<ActionResult<English_Test>> GetEnglishTest(int testId)
        {
            var englishTest = await _englishTestService.GetEnglishTest(testId);
            if (englishTest == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(englishTest);
        }

        [HttpPost]
        public async Task<ActionResult<English_Test>> CreateEnglishTest([FromBody] English_Test englishTest)
        {
            if (englishTest == null)
            {
                return BadRequest(new ApiResponse(400));
            }

            var createdTest = await _englishTestService.CreateEnglishTestAsync(englishTest);
            return CreatedAtAction(nameof(GetEnglishTest), new { testId = createdTest.TestID }, createdTest);
        }

        [HttpDelete("{testId}")]
        public async Task<IActionResult> DeleteEnglishTest(int testId)
        {
            var englishTest = await _englishTestService.GetEnglishTest(testId);
            if (englishTest == null)
            {
                return NotFound(new ApiResponse(404));
            }

            await _englishTestService.DeleteEnglishTest(testId);
            return NoContent();
        }
    }
}
