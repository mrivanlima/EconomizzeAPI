using AutoMapper;
using Economizze.Library;
using EconomizzeAPI.Model;
using EconomizzeAPI.Services.Repositories.Classes;
using EconomizzeAPI.Services.Repositories.Interfaces;
using EconomizzeHybrid.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EconomizzeAPI.Controllers
{
	[ApiController]
	[Route("api/orcamento")]
	public class QuoteController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IQuoteRepository _quoteRepository;
		public QuoteController(IQuoteRepository quoteRepository, IMapper mapper)
		{
			_quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		#region CREATE QUOTE
		[HttpPost("criar")]
		public async Task<ActionResult<Quote>> CreateUser(QuoteViewModel quote)
		{
			var map = _mapper.Map<Quote>(quote);

			var QuoteViewModel = await _quoteRepository.CreateQuoteAsync(map);
			if (QuoteViewModel.Item2.HasError)
			{
				return BadRequest(QuoteViewModel.Item2.Message);
			}

			return CreatedAtRoute(	"orcamento", 
									new { QuoteId = QuoteViewModel.Item1.QuoteId }, 
									_mapper.Map<QuoteViewModel>(map));
		}
		#endregion

		#region FIND NEIGHBORHOOD ID BY STREET ID
		[HttpGet("street/{streetId}", Name = "nid")]
		public async Task<ActionResult<Neighborhood>> GetByStreetId(int streetId)
		{
			var neighborhood = await _quoteRepository.FindNeighborhoodId(streetId);
			if (neighborhood == null)
			{
				return NotFound();
			}

			return Ok(_mapper.Map<Neighborhood>(neighborhood));
		}
		#endregion

		#region GET Quote ID
		[HttpGet("{QuoteId}", Name = "orcamento")]
		public async Task<ActionResult<QuoteViewModel>> GetQuoteById(int QuoteId)
		{
			var quote = "hi";
			if (quote == null)
			{
				return NotFound();
			}

			return Ok(quote);
		}
		#endregion
	}
}
