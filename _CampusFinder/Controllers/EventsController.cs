using _CampusFinder.Controllers;
using _CampusFinder.Errors;
using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderCore.Repositories.Contract;
using AutoMapper;
using CampusFinder.Dto.University_Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CampusFinder.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EventsController : BaseApiController
	{
		private readonly IGenericRepository<Events> _eventsRepo;
		private readonly IMapper _mapper;

		public EventsController(IGenericRepository<Events> eventsRepo , IMapper mapper)
        {
			_eventsRepo = eventsRepo;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<EventsDto>>> GetEvents()
		{
			var events = await _eventsRepo.GetAllAsync();
			if (events == null)
			{
				return NotFound(new ApiResponse(404).ToDictionary());
			}
			var eventsDtos = _mapper.Map<IEnumerable<Events>, IEnumerable<EventsDto>>(events);
			return Ok(eventsDtos);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult> GetEvent(int id)
		{
			var _event = await _eventsRepo.GetByIdAsync(id);
			if (_event == null)
			{
				return NotFound(new ApiResponse(404).ToDictionary());
			}
			var eventDto = _mapper.Map<Events, EventsDto>(_event);
			return Ok(eventDto);
		}
    }
}
