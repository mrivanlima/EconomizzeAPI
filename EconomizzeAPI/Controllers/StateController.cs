﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Economizze.Library;
using Microsoft.AspNetCore.Http.HttpResults;
using AutoMapper;
using EconomizzeAPI.Model;
using EconomizzeAPI.Services.Repositories.Interfaces;

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
		public async Task<ActionResult<State>> CreateState(StateViewModel state)
		{
			var map = _mapper.Map<State>(state);
			var stateViewModel = await _stateRepository.CreateStateAsync(map);
			if (stateViewModel.Item2.HasError)
			{
				return BadRequest();
			}

			return CreatedAtRoute("estado", new { StateId = stateViewModel.Item1.StateId }, _mapper.Map<StateViewModel>(map));
		}

		[HttpGet("{stateId}", Name = "estado")]
		public async Task<ActionResult<StateViewModel>> GetById(short stateId)
		{
			var state = await _stateRepository.ReadStateByIdAsync(stateId);
			if (state.StateId < 1)
			{
				return NotFound();
			}

			return Ok(_mapper.Map<StateViewModel>(state));
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<StateViewModel>>> GetStatesAll()
		{
			var states = await _stateRepository.ReadAllStatesAsync();
			if (states.Count() < 1)
			{
				return NotFound();
			}
			return Ok(_mapper.Map<IEnumerable<StateViewModel>>(states));
		}

		[HttpPut]
		public async Task<ActionResult<StateViewModel>> UpdateById(StateViewModel state)
		{
			var result = _mapper.Map<State>(state);
			var StateViewModel = await _stateRepository.UpdateStateAsync(result);
			if (StateViewModel == true)
			{
				return NotFound();
			}
			return NoContent();

		}

	}
}
