using _CampusFinder.Controllers;
using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderCore.Services.Contract;
using _CampusFinderCore.Specifications;
using AutoMapper;
using CampusFinder.Dto.University_Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CampusFinder.Controllers
{
    public class DiplomaController : BaseApiController
    {
        private readonly IDiplomaService _diplomaService;
        private readonly IMapper _mapper;

        public DiplomaController(IDiplomaService diplomaService , IMapper mapper)
        {
            _diplomaService = diplomaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiplomaDto>>> GetDiplomas()
        {
            var diplomas = await _diplomaService.GetDiplomasAsync();
            if (diplomas == null || !diplomas.Any())
            {
                return NotFound();
            }
            var diplomaDtos = _mapper.Map<IEnumerable<Diploma>,IEnumerable<DiplomaDto>>(diplomas);
            return Ok(diplomaDtos);
        }

        [HttpGet("{diplomaId}")]
        public async Task<ActionResult<DiplomaDto>> GetDiploma(int diplomaId)
        {
            var diploma = await _diplomaService.GetDiplomaAsync(diplomaId);
            if (diploma == null)
            {
                return NotFound();
            }
            var diplomaDto = _mapper.Map<Diploma, DiplomaDto>(diploma);
            return Ok(diplomaDto);
        }

        [HttpPost]
        public async Task<ActionResult<DiplomaDto>> CreateDiploma([FromBody]DiplomaDto diploma)
        {
            var diplomaEntity = _mapper.Map<DiplomaDto, Diploma>(diploma);
            var createdDiploma = await _diplomaService.CreateDiplomaAsync(diplomaEntity);
            var createdDiplomaDto = _mapper.Map<Diploma, DiplomaDto>(createdDiploma);
            return CreatedAtAction(nameof(GetDiploma), new { diplomaId = createdDiplomaDto.Id }, createdDiplomaDto);
        }

        [HttpDelete("{diplomaId}")]
        public async Task<IActionResult> DeleteDiploma(int diplomaId)
        {
            
            var diploma = await _diplomaService.GetDiplomaAsync(diplomaId);
            if (diploma == null)
            {
                return NotFound();
            }
            await _diplomaService.DeleteDiplomaAsync(diplomaId);
            return NoContent();
        }
    }
}
