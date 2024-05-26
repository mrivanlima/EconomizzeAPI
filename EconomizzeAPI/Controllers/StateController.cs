using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Economizze.Library;
using EconomizzeAPI.Services.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EconomizzeAPI.Controllers
{
	[ApiController]
	[Route("api/estado")]
	public class StateController  : ControllerBase
	{
		private readonly IStateRepository _stateRepository;
		public StateController(IStateRepository stateRepository)
		{
			_stateRepository = stateRepository ?? throw new ArgumentNullException(nameof(stateRepository)); ;
		}

		[HttpPost(Name = "Criar")]
		public async Task<ActionResult<State>> CreateState(State state)
		{
			//var result = _mapper.Map<State>(state);
			var results = await _stateRepository.CreateAsync(state);
			if (!results.Item2)
			{
				return null; // CreatedAtRoute();

			}
			//if (StateViewModel == null)
			//{
			//	return BadRequest();
			//}
			//return CreatedAtRoute("estado", new { StateId = result.StateId }, _mapper.Map<StateViewModel>(result));
			return Ok();
		}

	}
}
