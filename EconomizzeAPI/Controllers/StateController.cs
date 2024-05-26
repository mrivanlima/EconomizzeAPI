using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Economizze.Library;
using EconomizzeAPI.Services.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using AutoMapper;
using EconomizzeAPI.Model;

namespace EconomizzeAPI.Controllers
{
	[ApiController]
	[Route("api/estado")]
	public class StateController  : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IStateRepository _stateRepository;
		public StateController(IStateRepository stateRepository, IMapper mapper)
		{
			_stateRepository = stateRepository ?? throw new ArgumentNullException(nameof(stateRepository));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		[HttpPost]
		public async Task<ActionResult<State>> CreateState(State state)
		{
			var map = _mapper.Map<State>(state);
			var stateViewModel = await _stateRepository.CreateAsync(map);
			if (stateViewModel.Item2)
			{
				return BadRequest();

			}

			return CreatedAtRoute("estado", new { StateId = stateViewModel.Item1.StateId }, _mapper.Map<StateViewModel>(map));
		}

		[HttpGet("{stateId}")]
		public async Task<ActionResult<StateViewModel>> GetById(short stateId)
		{
			var state = await _stateRepository.ReadByIdAsync(stateId);
			if (state.StateId < 1)
			{
				return NotFound();
			}

			return Ok(_mapper.Map<StateViewModel>(state));
		}

		[HttpPut]
		public async Task<ActionResult<StateViewModel>> UpdateById(StateViewModel state)
		{
			//var result = _mapper.Map<State>(state);
			//var StateViewModel = await _stateRepository.UpdateStateAsyncById(result);
			//if (StateViewModel == null)
			//{
			//	return NotFound();
			//}
			//return NoContent();

			return NoContent();
		}

	}
}
