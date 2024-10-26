using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Economizze.Library;
using Microsoft.AspNetCore.Http.HttpResults;
using AutoMapper;
using EconomizzeAPI.Model;
using EconomizzeAPI.Services.Repositories.Interfaces;
using EconomizzeAPI.Services.Repositories.Classes;

namespace EconomizzeAPI.Controllers
{
    [ApiController]
    [Route("api/profissao")]
    public class ProfessionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProfessionRepository _professionRepository;
        public ProfessionController(IProfessionRepository professionRepository, IMapper mapper)
        {
            _professionRepository = professionRepository ?? throw new ArgumentNullException(nameof(professionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<ActionResult<Profession>> CreateProfession(ProfessionViewModel profession)
        {
            var map = _mapper.Map<Profession>(profession);
            var professionViewModel = await _professionRepository.CreateProfessionAsync(map);
            if (professionViewModel.Item2.HasError)
            {
                return BadRequest();
            }

            return CreatedAtRoute("profissao", new { ProfessionId = professionViewModel.Item1.ProfessionId }, _mapper.Map<ProfessionViewModel>(map));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfessionViewModel>>> ReadAllProfessions()
        {
            var professions = await _professionRepository.ReadAllProfessionsAsync();
            if (professions.Count() < 1)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<ProfessionViewModel>>(professions));
        }

        [HttpGet("{professionId}", Name = "profissao")]
        public async Task<ActionResult<ProfessionViewModel>> GetById(short professionId)
        {
            throw new NotImplementedException();
        }
    }
}
