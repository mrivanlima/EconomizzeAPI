using AutoMapper;
using Economizze.Library;
using EconomizzeAPI.Model;
using EconomizzeAPI.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EconomizzeAPI.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<ActionResult<State>> CreateState(UserSetUp user)
        {
            var map = _mapper.Map<UserSetUp>(user);
            var userSetUpViewModel = await _userRepository.CreateAsync(map);
            if (userSetUpViewModel.Item2)
            {
                return BadRequest();

            }

            return CreatedAtRoute("usuario", new { UserId = userSetUpViewModel.Item1.UserId }, _mapper.Map<UserSetUpViewModel>(map));
        }

        [HttpGet("{userId}", Name = "usuario")]
        public async Task<ActionResult<StateViewModel>> GetById(int userId)
        {
            var state = await _userRepository.ReadByIdAsync(userId);
            if (state.StateId < 1)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<StateViewModel>(state));
        }
    }
}
